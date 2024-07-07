using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.middleware;
using API.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt=>
{
    opt.UseSqlite("Data Source=datingapp.db");
});
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();//.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);;
builder.Services.AddCors();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((options)=>
{
    options.TokenValidationParameters=new TokenValidationParameters
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ngit@123$......................................................................................................................................................../1.2/1.2/1./1.21/.2/1.2/1.2/.1")),
            ValidateIssuer=false,
            ValidateAudience=false
        };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenCreation,TokenGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

var _scope=app.Services.CreateScope();
var _services=_scope.ServiceProvider;
try
{
    DataContext _datacontext=_services.GetService<DataContext>();
    await _datacontext.Database.MigrateAsync();
    await Seed.seedUsers(_datacontext);
}
catch (Exception ex)
{
    ILogger _logger=_services.GetService<ILogger<Program>>();
    _logger.LogError(ex,"An error occured");
}

app.Run();

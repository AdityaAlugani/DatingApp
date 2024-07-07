using System.Net.Mime;
using System.Text.Json;
using API.Error;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.middleware
{
    public class ErrorHandlingMiddleware
    {

        private readonly ILogger  _logger;
        private readonly RequestDelegate _requestDelegate;
        private bool _env;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger,RequestDelegate requestDeligate)
        {
            _logger = logger;
            _requestDelegate=requestDeligate;
            _env=Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _requestDelegate(httpcontext);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                httpcontext.Response.StatusCode =500;
                httpcontext.Response.ContentType=MediaTypeNames.Application.Json;
                //const JsonSerializerOptions jsonserializeoptions=new JsonSerializerOptions();
                //var options=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                //_logger.LogWarning(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                var errordevelopment=new Errorpropogate(e.Message,e.StackTrace.ToString(),httpcontext.Response.StatusCode);
                var errorproduction=new Errorpropogate(e.Message,"Internal Server Error",httpcontext.Response.StatusCode);
                _logger.LogWarning(JsonConvert.SerializeObject(errordevelopment));
                DefaultContractResolver options = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                var jsonresponse=_env ? JsonConvert.SerializeObject(errordevelopment,new JsonSerializerSettings{ContractResolver=options}) : JsonConvert.SerializeObject(errorproduction,new JsonSerializerSettings{ContractResolver=options});

                await httpcontext.Response.WriteAsync(jsonresponse);
            }
        }
    }
}
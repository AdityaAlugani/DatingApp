using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.data
{
    public class Seed
    {
        public static async Task seedUsers(DataContext _dataContext)
        {
            if(await _dataContext.Users.AnyAsync())
            return;

            string alluserstext=await File.ReadAllTextAsync("data/UserSeedData.json");
            Console.Write("Custom string:"+alluserstext+"\n");
            var options=new JsonSerializerOptions
            {
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase
            };
            var allUsers=JsonSerializer.Deserialize<List<AppUser>>(alluserstext);
            //Console.Write(allUsers+"\n");

            foreach(AppUser user in allUsers)
            {
                Console.WriteLine(user.UserName);
                var hash=new HMACSHA512();
                user.PasswordHash=hash.ComputeHash(Encoding.UTF8.GetBytes("ngit"));
                user.PasswordSalt=hash.Key;

                await _dataContext.Users.AddAsync(user);
                await _dataContext.SaveChangesAsync();
            }

        }
    }
}
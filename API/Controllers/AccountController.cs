
using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Interfaces;
namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenCreation _tokenCreation;

        public AccountController(DataContext context,ITokenCreation tokenCreation)
        {
            _context=context;
            _tokenCreation=tokenCreation;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync((user)=>user.UserName.ToLower()==username.ToLower());
        }
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterUser(RegisterDto register)
        {
            if(await UserExists(register.Username))
            return BadRequest("Username is taken");


            using var hmac=new HMACSHA512();
            AppUser appuser=new AppUser{
                UserName=register.Username,
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt=hmac.Key
            };
            //Console.WriteLine(appuser);
            _context.Add(appuser);
            await _context.SaveChangesAsync();
            return appuser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> PasswordVerify(LoginDto login)
        {
            AppUser RetrivedUser=await _context.Users.FirstOrDefaultAsync(x=>x.UserName==login.UserName);
            if(RetrivedUser==null)
            return Unauthorized("Username or password is wrong");

            using var hmac=new HMACSHA512(RetrivedUser.PasswordSalt);
            byte[] computedhash=hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for(int i=0;i<computedhash.Length;i++)
            {
                if(RetrivedUser.PasswordHash[i] != computedhash[i])
                return Unauthorized("Username or password is wrong!");
            }
            string token=_tokenCreation.CreateToken(RetrivedUser);
            UserDto userResponse=new UserDto
            {
                UserName=RetrivedUser.UserName,
                Token=token
            };
            return Ok(userResponse);
        }
        
    }
}
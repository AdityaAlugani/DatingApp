
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenGenerator : ITokenCreation
    {
        private SymmetricSecurityKey secretkey;
        public TokenGenerator(IConfiguration configuration)
        {
            secretkey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ngit@123$......................................................................................................................................................../1.2/1.2/1./1.21/.2/1.2/1.2/.1"));
        }

        public string CreateToken(AppUser user)
        {

            var claim=new List<Claim>() {new Claim(JwtRegisteredClaimNames.NameId,user.UserName)};
            var tokenDescriptor=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claim),
                SigningCredentials = new SigningCredentials(secretkey,SecurityAlgorithms.HmacSha512Signature),
                Expires=DateTime.Now.AddDays(30)
            };
            var Tokenhandler=new JwtSecurityTokenHandler();
            var token=Tokenhandler.CreateToken(tokenDescriptor);
            return Tokenhandler.WriteToken(token);
        }
    }
}
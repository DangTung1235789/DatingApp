using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //Adding the create token logic
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //TokenKey in appsettings.Development.json
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //adding our claims 
            var claims = new List<Claim>
            {
                //13. Making the Last Active action filter more optimal
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            //create some credentials 
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //describing how Token look
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            // We need this token handler   
            var tokenHandler = new JwtSecurityTokenHandler();
            //create token 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return token 
            return tokenHandler.WriteToken(token);
        }
    }
}
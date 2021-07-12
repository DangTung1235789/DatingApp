using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //16.9. Adding the roles to the JWT token
        //Adding the create token logic
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            //TokenKey in appsettings.Development.json
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        

        public async Task<string> CreateToken(AppUser user)
        {
            //adding our claims 
            var claims = new List<Claim>
            {
                //13. Making the Last Active action filter more optimal
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
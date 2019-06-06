using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class JWTGeneratorByIdUserService : ITokenGeneratorService
    {
        private readonly ISecurityValidatorService _secretKeyFinderService;
        public JWTGeneratorByIdUserService(ISecurityValidatorService secretKeyFinderService)
        {
            _secretKeyFinderService = secretKeyFinderService;
        }

        public string Generate(string Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();            
            var key = Encoding.ASCII.GetBytes(_secretKeyFinderService.GetSecrets().First());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}

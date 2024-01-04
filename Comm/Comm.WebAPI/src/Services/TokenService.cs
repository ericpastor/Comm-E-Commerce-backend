using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Comm.WebAPI.src.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config; //Añadimos este y su ctor(TokenService con el IConfiguration) para tener el config object
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(User user)
        {
            var issuer = _config.GetSection("Jwt:Issuer").Value;
            var claims = new List<Claim>{           //Claim viene de system
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   //Todas estos Claims son values que puedes leer
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                // new Claim(ClaimTypes.Email, user.Email),
            };
            var audience = _config.GetSection("Jwt:Audience").Value;
            var tokenHandler = new JwtSecurityTokenHandler();    //viene de system
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); //Algorithms viene del package identityModel.Token.Jwt
            var descriptor = new SecurityTokenDescriptor  //SecurityTokenDescriptor viene del package identityModel.Token.Jwt
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddDays(2),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingKey
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
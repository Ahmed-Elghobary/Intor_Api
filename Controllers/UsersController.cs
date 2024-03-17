using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiBeginner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(JwtOptions jwtOptions) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string> AuthenticateUser(UserModel userRequest)
        {
            var tokenHandel = new JwtSecurityTokenHandler();
            //data about token and user
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256),
                Subject= new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier,userRequest.UserName),
                    new (ClaimTypes.Email,"ahmed@gmail")
                })
            };
            var securityToke=tokenHandel.CreateToken(tokenDescriptor);
            var accessToke= tokenHandel.WriteToken(securityToke);
            return Ok(accessToke);
        }
    }
}

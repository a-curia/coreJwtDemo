using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("token")]
        public ActionResult GetToken()
        {
            // security key
            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_f$3224.in";

            // symetic security key
            var symmeticSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            // signing credential
            var signingCredentials = new SigningCredentials(symmeticSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role,"Administrator"));
            claims.Add(new Claim(ClaimTypes.Role, "Reader"));
            claims.Add(new Claim("OurCustomClaim","CutomClaimValue"));

            // create token
            var token = new JwtSecurityToken(
                    issuer:"anystring.com",
                    audience: "any string readers",
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials,
                    // assign claims to out token
                    claims: claims
                );

            // return token
            var strToken  = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(strToken);
        }
    }
}
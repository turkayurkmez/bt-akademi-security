using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecurityInREST.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityInREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Validate(model.UserName, model.Password);
                if (user != null)
                {
                    //token'ı üret ve istemciye bildir.
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-ifade-token-onayi-icindir-ona-gore"));
                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)

                    };

                    var token = new JwtSecurityToken(
                        issuer:"server.btakademi",
                        audience: "client.btakademi",
                        claims: claims,
                        notBefore:DateTime.Now,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credential);

                    return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
                }
                ModelState.AddModelError("login", "Hatalı giriş");
            }
            return BadRequest(ModelState);
        }
    }
}

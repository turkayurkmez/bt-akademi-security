using AuthNAndAuthZ.Models;
using AuthNAndAuthZ.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthNAndAuthZ.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IUserService userService;

        public KullaniciController(IUserService userService)
        {
            this.userService = userService;
        }

      

        public IActionResult Giris(string? gidilecekAdres)
        {
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel() { ReturnUrl = gidilecekAdres };

            return View(userLoginViewModel);
        }

        [HttpPost]
        public  async Task<IActionResult> Giris(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.ValidateUser(model.UserName, model.Password);
                if (user != null)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(principal);

                    if (!string.IsNullOrEmpty(model.ReturnUrl)&& Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("login", "Login Failed");

            }
            return View();
        }

        [HttpGet]
        public async Task< IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult ErisimEngellendi() => View();


        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(CreateNewUserModel model)
        {
            
            if (ModelState.IsValid)
            {
                userService.CreateUser(model);
                return Redirect("/");
            }
            return View();
        }
    }
}

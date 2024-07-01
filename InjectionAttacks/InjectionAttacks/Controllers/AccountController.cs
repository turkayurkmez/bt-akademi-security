using InjectionAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Encodings.Web;

namespace InjectionAttacks.Controllers
{
    public class AccountController : Controller
    {
        private readonly JavaScriptEncoder javaScriptEncoder;

        public AccountController(JavaScriptEncoder javaScriptEncoder)
        {
            this.javaScriptEncoder = javaScriptEncoder;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            /*
               SELECT * FROM Users WHERE UserName='turkay' AND Password='123456'             * 
             */

            var connection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True;");
            var command = new SqlCommand("SELECT * FROM Users WHERE UserName=@name AND Password=@password",connection);

            connection.Open();
            command.Parameters.AddWithValue("@name", user.UserName);
            command.Parameters.AddWithValue("@password", user.Password);


            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewBag.State = "Giriş Başarılı";
                return View();
            }


            ViewBag.State = "Giriş Başarısız";
            return View();



        }

        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(UserRegisterViewModel userRegister)
        {
            if (ModelState.IsValid)
            {
                userRegister.UserInfo = javaScriptEncoder.Encode(userRegister.UserInfo);
                return View(userRegister);
            }
           return View();
        }

    }
}

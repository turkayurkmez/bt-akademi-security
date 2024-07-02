using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNAndAuthZ.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class ForbiddenController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

     
        public IActionResult Create()
        {
            return View();
        }
    }
}

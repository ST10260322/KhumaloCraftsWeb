using Microsoft.AspNetCore.Mvc;
using KhumaloCraftsWeb.Models;

namespace KhumaloCraftsWeb.Controllers
{
    public class userController : Controller
    {
        public userTable usrtbl = new userTable();



        [HttpPost]
        public ActionResult UserSignIn(userTable Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UserSIgnIn()
        {
            return View(usrtbl);
        }

    }

}

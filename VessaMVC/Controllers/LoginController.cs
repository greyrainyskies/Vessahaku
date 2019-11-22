using Microsoft.AspNetCore.Mvc;
using VessahakuAPI.Models;

namespace VessaMVC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Käyttäjät objUser)
        {
            var käyttäjänimi = objUser.Nimimerkki;

            //if (ModelState.IsValid)
            //{

            //        var obj = käyttäjät.Nimimerkki.Where(a => a.Equals(objUser.Nimimerkki) && a.Equals(objUser.Salasana)).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            HttpContext.Session.SetInt32("KäyttäjäId", objUser.KäyttäjäId);
            //            HttpContext.Session.SetString("Nimimerkki", objUser.Nimimerkki);
            //       string käyttäjänimi = HttpContext.Session.GetString("Nimimerkki");
            ViewBag.Nimimerkki = käyttäjänimi;
                    return View("UserDashBoard");
            //        }
                
            //}
            //return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if ("KäyttäjäId" != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
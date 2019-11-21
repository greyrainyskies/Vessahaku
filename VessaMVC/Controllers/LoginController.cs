using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VessahakuAPI.Models;
using Microsoft.AspNetCore.Http;

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
            Käyttäjät käyttäjät = new Käyttäjät();

            if (ModelState.IsValid)
            {

                    var obj = käyttäjät.Nimimerkki.Where(a => a.Equals(objUser.Nimimerkki) && a.Equals(objUser.Salasana)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetInt32("KäyttäjäId", objUser.KäyttäjäId);
                        HttpContext.Session.SetString("Nimimerkki", objUser.Nimimerkki);
                   string käyttäjänimi = HttpContext.Session.GetString("Nimimerkki");
                    ViewBag.Nimimerkki = käyttäjänimi;
                    return RedirectToAction("UserDashBoard");
                    }
                
            }
            return View(objUser);
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
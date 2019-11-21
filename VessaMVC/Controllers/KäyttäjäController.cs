using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VessaMVC.Models;

namespace VessaMVC.Controllers
{
    public class KäyttäjäController : Controller
    {
        Jsonkäsittelykäyttäjä j = new Jsonkäsittelykäyttäjä();

        // GET: Käyttäjä
        public ActionResult TulostaKäyttäjät()
        {
            string json = j.Jsonhommatkäyttäjä();
            List<Käyttäjät> käyttäjät;
            käyttäjät = JsonConvert.DeserializeObject<List<Käyttäjät>>(json);
            return View(käyttäjät);
        }

        // GET: Käyttäjä/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Käyttäjä/Create
       [HttpGet]
        public ActionResult Lisää()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Lisää(Käyttäjät käyttäjä)
        {

            string url = $"https://localhost:44330/api/käyttäjä/Lisääkäyttäjä";

            string body = JsonConvert.SerializeObject(käyttäjä);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(body, UTF8Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TulostaKäyttäjät", "Käyttäjä");
                }
                else
                {
                    ModelState.AddModelError("", "Jotain meni pieleen.");
                    return View();
                }


            }
        }
      

        // POST: Käyttäjä/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Käyttäjä/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Käyttäjä/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Käyttäjä/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Käyttäjä/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
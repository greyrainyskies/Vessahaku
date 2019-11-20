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
    public class VessaController : Controller
    {
        Jsonkäsittely j = new Jsonkäsittely();
        // GET: Vessa
        public ActionResult TulostaKaikki()
        {
            
            string json=j.Jsonhommat();
            List<Wct> wc;
            wc = JsonConvert.DeserializeObject<List<Wct>>(json);
            return View(wc);
           
        }

        // GET: Vessa/Details/5
        public ActionResult Details(int id)
        {
            string json = j.Jsonhommat(id:id, urlinloppu:"tiedot/");
            Wct wc= JsonConvert.DeserializeObject<Wct>(json);
            return View(wc);
        }

        // GET: Vessa/Create
        public ActionResult LisaaWc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisaaWc(Wct wc)
        {

            string url = $"https://localhost:44330/api/vessa";

            string body = JsonConvert.SerializeObject(wc);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(body, UTF8Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Jotain meni pieleen.");
                    return View(wc);
                }
                 

            }
        }


        // POST: Vessa/Create
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

        // GET: Vessa/Edit/5
        public ActionResult Edit(int id)
        {
            string json = j.Jsonhommat(id:id, urlinloppu:"Tiedot/");
            Wct wc;
            wc = JsonConvert.DeserializeObject<Wct>(json);
            return View(wc);
           
        }

        // POST: Vessa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wct wc, int id)
        {
            //string url = $"https://localhost:44330/api/vessa/";

            //string body = JsonConvert.SerializeObject(wc);

           
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(wc);
                
                    client.DefaultRequestHeaders.Accept.Add(new
                   MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PostAsync($"https://localhost:44330/api/vessa/{id}", content).Result;
                    //json = response.Content.ReadAsStringAsync().Result;

                   
                
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", id);
                }
                else
                {
                    ModelState.AddModelError("", "Jotain meni pieleen.");
                    return View(wc);
                }
            }
        }
        // GET: Vessa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vessa/Delete/5
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
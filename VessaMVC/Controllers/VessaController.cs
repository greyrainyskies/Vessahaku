using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public ActionResult Create()
        {
            return View();
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
            return View();
        }

        // POST: Vessa/Edit/5
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
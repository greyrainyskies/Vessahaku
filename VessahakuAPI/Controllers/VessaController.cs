using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using VessahakuAPI.Models;

namespace VessahakuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VessaController : ControllerBase
    {
        VessatContext db = new VessatContext();
        // GET: api/Vessa
        [HttpGet]
        public IEnumerable<Wct> Get()
        {
            
            var vessat = from a in db.Wct
                         select a;

            return vessat.ToList();
        }
        [HttpGet("Tiedot/{id}", Name = "HakuID")]
        public Wct GetIdllä(int id)
        {
             return db.Wct.Find(id);
        }
        // GET: api/Vessa/5
        [HttpGet("Haku/{nimi}", Name = "Haku")]
        public IEnumerable<Wct> GetNimellä(string nimi)
        {

            var a = db.Wct.Where(s => s.Nimi.ToLower().Contains(nimi.ToLower())).ToList();
            return a;
        }
        [HttpGet("Haku/{pnro}", Name = "Hakupostin")]
        public IEnumerable<Wct> Getpnrolla(string pnro)
        {
            var a = db.Wct.Where(s => s.Postinro.Contains(pnro)).ToList();
            return a;
        }
        [HttpGet("Haku/{longi}/{lat}", Name = "Hakuetäisyydellä")]
        public IEnumerable<Wct> Getpnrolla(decimal longi, decimal lat)
        {
            Coordinate c = new Coordinate(Convert.ToDouble(longi), Convert.ToDouble(lat));
            var a = db.Wct.OrderBy(s => s.Sijainti.Distance(new Point(c) { SRID = 4326 })).ToList();

            return a;
        }


        // POST: api/Vessa
        [HttpPost]
        public void LisääWC([FromBody] Wct wc)
        {
            int tid = db.Wct.Max(p => p.WcId + 1);
            wc.WcId = tid;
            db.Add(wc);
            db.SaveChanges();
        }

        // PUT: api/Vessa/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Wct wc)
        {

            db.Update(wc);
            db.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

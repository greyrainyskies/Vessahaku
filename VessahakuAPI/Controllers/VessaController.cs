using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using VessahakuAPI.Models;
using OsoiteGPS;

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

        [HttpGet("Lahimmat/{lat}/{lon}", Name = "Lähimmät sijainnista")]
        public IEnumerable<Wct> LähimmätSijainnista(double lat, double lon)
        {
            var c = new Coordinate(lon, lat);
            var a = db.Wct.OrderBy(wc => wc.Sijainti.Distance(new Point(c) { SRID = 4326 })).ToList();
            return a;
        }

        [HttpGet("Lahimmat/{paikka}", Name = "Lähimmät paikasta")]
        public IEnumerable<Wct> LähimmätPaikasta(string paikka)
        {
            var sijainti = Osoite.Haku(paikka);
            var a = db.Wct.OrderBy(wc => wc.Sijainti.Distance(sijainti)).ToList();
            return a;
        }

        [HttpPost]
        public IActionResult LisääWC([FromBody] Wct wc)
        {
            try
            {
                var uusi = new Wct();
                uusi.Nimi = wc.Nimi;
                uusi.Katuosoite = wc.Katuosoite;
                uusi.Postinro = wc.Postinro;
                uusi.Kaupunki = wc.Kaupunki;
                var sijainti = Osoite.Haku(uusi.Katuosoite, uusi.Postinro, uusi.Kaupunki);
                uusi.Lat = Convert.ToDecimal(sijainti.Coordinates.First().Y);
                uusi.Long = Convert.ToDecimal(sijainti.Coordinates.First().X);
                uusi.Sijainti = sijainti;
                uusi.Ilmainen = wc.Ilmainen;
                uusi.Unisex = wc.Unisex;
                uusi.Saavutettava = wc.Saavutettava;
                uusi.Aukioloajat = wc.Aukioloajat;
                uusi.Koodi = wc.Koodi;
                uusi.Ohjeet = wc.Ohjeet;
                db.Wct.Add(uusi);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
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

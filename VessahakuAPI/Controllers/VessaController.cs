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

        [HttpGet("Lahimmat/{lat}/{lon}", Name = "Lähimmät sijainnista")]
        public IEnumerable<Wct> LähimmätSijainnista(double lat, double lon, int? maara, string postinumero, string kaupunki)
        {
            var sijainti = new Point(lon, lat) { SRID = 4326 };
            return LähimmätSijainnista(sijainti, maara, postinumero, kaupunki);
        }

        private IEnumerable<Wct> LähimmätSijainnista(Point sijainti, int? määrä, string postinumero, string kaupunki)
        {
            var lista = db.Wct.OrderBy(wc => wc.Sijainti.Distance(sijainti)).ToList();
            if (!string.IsNullOrWhiteSpace(postinumero))
            {
                lista = lista.Where(wc => wc.Postinro == postinumero.Trim()).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(kaupunki))
            {
                lista = lista.Where(wc => wc.Kaupunki.ToLower() == kaupunki.Trim().ToLower()).ToList();
            }
            if (määrä != null)
            {
                return lista.Take(määrä.GetValueOrDefault());
            }
            else
            {
                return lista;
            }
        }

        [HttpGet("Lahimmat/{paikka}", Name = "Lähimmät paikasta")]
        public IEnumerable<Wct> LähimmätPaikasta(string paikka, int? maara, string postinumero, string kaupunki)
        {
            var sijainti = Osoite.Haku(paikka);
            return LähimmätSijainnista(sijainti, maara, postinumero, kaupunki);
        }

        [HttpPost]
        public IActionResult LisääWC([FromBody] Wct wc)
        {
            try
            {
                var uusi = new Wct();
                uusi.Nimi = wc.Nimi;
                uusi.Katuosoite = wc.Katuosoite;
                uusi.Kaupunki = wc.Kaupunki;
                try
                {
                    uusi.Postinro = Osoite.Postinumero(uusi.Katuosoite, uusi.Kaupunki);
                }
                catch (ArgumentException)
                {
                    uusi.Postinro = wc.Postinro;
                }
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
                return BadRequest(e.Message); // Poista e.Message ennen tuotantoa
            }
        }

        // PUT: api/Vessa/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Wct wc)
        {
            try
            {
                var muutettava = db.Wct.Find(id);
                muutettava.Nimi = wc.Nimi.Trim();
                muutettava.Katuosoite = wc.Katuosoite.Trim();
                muutettava.Postinro = wc.Postinro.Trim();
                muutettava.Kaupunki = wc.Kaupunki.Trim();
                var sijainti = Osoite.Haku(muutettava.Katuosoite, muutettava.Postinro, muutettava.Kaupunki);
                muutettava.Lat = Convert.ToDecimal(sijainti.Coordinates.First().Y);
                muutettava.Long = Convert.ToDecimal(sijainti.Coordinates.First().X);
                muutettava.Sijainti = sijainti;
                muutettava.Ilmainen = wc.Ilmainen;
                muutettava.Unisex = wc.Unisex;
                muutettava.Saavutettava = wc.Saavutettava;
                muutettava.Aukioloajat = wc.Aukioloajat.Trim();
                muutettava.Koodi = wc.Koodi.Trim();
                muutettava.Ohjeet = wc.Ohjeet.Trim();
                muutettava.Muokattu = DateTime.Now;
                db.Update(muutettava);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

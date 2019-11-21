using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VessahakuAPI.Models;

namespace VessahakuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KäyttäjäController : ControllerBase
    {
        VessatContext db = new VessatContext();

        // GET: api/Käyttäjä
        [HttpGet]
        public IEnumerable<Käyttäjät> Get()
        {

            var käyttäjät = from a in db.Käyttäjät
                         select a;

            return käyttäjät.ToList();
        }

        // GET: api/Käyttäjä/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Käyttäjä
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Käyttäjä/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT: api/Käyttäjä/5
        [HttpGet("KTiedot/{id}", Name = "KTiedot")]
        public Käyttäjät Tiedot(int id)
        {
            return db.Käyttäjät.Find(id);
        }
        [HttpPost]
        public IActionResult LisääKäyttäjä([FromBody] Käyttäjät käyttäjä )
        {
            try
            {
                var uusi = new Käyttäjät();
                uusi.Nimimerkki = SiistiRivi(käyttäjä.Nimimerkki);
                uusi.Salasana = SiistiRivi(käyttäjä.Salasana);
                uusi.Sähköposti = SiistiRivi(käyttäjä.Sähköposti);
               

                db.Käyttäjät.Add(uusi);
                db.SaveChanges();
                return CreatedAtAction("KTiedot", "Käyttäjä", new { id = uusi.KäyttäjäId }, uusi);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); // Poista e.Message ennen tuotantoa
            }
        }
        private string SiistiRivi(string teksti)
        {
            if (!string.IsNullOrEmpty(teksti))
            {
                var trimmattu = teksti.Trim();
                return trimmattu.Substring(0, 1).ToUpper() + trimmattu.Substring(1);
            }
            else
            {
                return teksti;
            }
        }

    }
}

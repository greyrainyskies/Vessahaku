using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace OsoiteGPS
{
    public static class Osoite
    {
        const string baseURL = "https://api.digitransit.fi/geocoding/v1/";
        public static Point Haku(string hakutekijät)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(baseURL + "search?text=" + HttpUtility.UrlEncode(hakutekijät)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var sijainti = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result).Features.First();
                    var lon = sijainti.Geometry.Coordinates.First();
                    var lat = sijainti.Geometry.Coordinates.Last();
                    return new Point(lon, lat) { SRID = 4326 };
                }
                else
                {
                    throw new ArgumentException("Virheelliset hakutekijät. Yritä uudelleen.");
                }

            }
        }

        public static Point Haku(string katuosoite, string postinumero, string kaupunki)
        {
            try
            {
                return Haku($"{katuosoite}, {postinumero} {kaupunki}");
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Osoitetieto virheellinen. Yritä uudelleen.");
            }
        }
    }
}
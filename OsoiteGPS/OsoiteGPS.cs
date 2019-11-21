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
        public static (string osoite, Point sijainti) Haku(string hakutekijät)
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
                    string osoite = "";
                    if (sijainti.Properties.Confidence >= 0.8M)
                    {
                        osoite = sijainti.Properties.Label;
                    }
                    return (osoite, new Point(lon, lat) { SRID = 4326 });
                }
                else
                {
                    throw new ArgumentException("Virheelliset hakutekijät. Yritä uudelleen.");
                }

            }
        }

        public static (string, Point) Haku(string katuosoite, string postinumero, string kaupunki)
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

        public static string Postinumero(string katuosoite, string kaupunki)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(baseURL + "search?text=" + HttpUtility.UrlEncode($"{katuosoite}, {kaupunki}")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var sijainti = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result).Features.First();
                    if (sijainti.Properties.Confidence >= 0.85M)
                    {
                        return sijainti.Properties.Postalcode;
                    }
                    else
                    {
                        throw new ArgumentException("Sopivaa postinumeroa ei löytynyt.");
                    }
                }
                else
                {
                    throw new ArgumentException("Postinumerohaku ei onnistunut.");
                }
            }
        }

        public static string SijainninPerusteella(decimal lat, decimal lon)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(baseURL + "reverse?point.lat=" + lat.ToString().Replace(',','.') + "&point.lon=" + lon.ToString().Replace(',', '.') + "&size=1").Result;
                if (response.IsSuccessStatusCode)
                {
                    var sijainti = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result).Features.First();
                    if (sijainti.Properties.Confidence >= 0.70M)
                    {
                        return sijainti.Properties.Label;
                    }
                    else
                    {
                        throw new ArgumentException("Sijainnin perusteella ei pysty antamaan osoitetta.");
                    }
                }
                else
                {
                    throw new ArgumentException("Sijainnin perusteella ei pysty antamaan osoitetta.");
                }
            }
        }
    }
}
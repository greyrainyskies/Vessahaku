using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VessaMVC.Models
{
    public class Jsonkäsittely
    {
        const string url ="https://localhost:44330/api/vessa/";
        public string Jsonhommat(string nimi = null, int id = -1, string urlinloppu=null)
        {
            
            string json = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (nimi != null)
                {
                    var response = client.GetAsync($"{url}{nimi}").Result;
                    return json = response.Content.ReadAsStringAsync().Result;

                }

                else if (id!=-1)
                {
                    var response = client.GetAsync($"{url}{urlinloppu}{id}").Result;
                    return json = response.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    var response = client.GetAsync($"{url}").Result;
                return json = response.Content.ReadAsStringAsync().Result;

                }
            }
        }

        public Results Lahimmat(decimal lat, decimal lon, int? maara, string postinumero, string kaupunki)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var kokoUrl = url + "Lahimmat/" + HttpUtility.UrlEncode(lat.ToString("G8")) + "/" + HttpUtility.UrlEncode(lon.ToString("G8")) + TeeQueryString(maara, postinumero, kaupunki);
                var response = client.GetAsync(kokoUrl).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Results>(json);
            }
        }

        public Results Lahimmat(string paikka, int? maara, string postinumero, string kaupunki)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var kokoUrl = url + "Lahimmat/" + paikka + TeeQueryString(maara, postinumero, kaupunki);
                var response = client.GetAsync(kokoUrl).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Results>(json);
            }
        }

        private string TeeQueryString(int? maara, string postinumero, string kaupunki)
        {
            var parametrit = new List<string>();
            if (maara.GetValueOrDefault() > 0)
            {
                parametrit.Add("maara=" + maara.ToString());
            }
            if (!string.IsNullOrWhiteSpace(postinumero))
            {
                parametrit.Add("postinumero=" + postinumero);
            }
            if (!string.IsNullOrWhiteSpace(kaupunki))
            {
                parametrit.Add("kaupunki=" + kaupunki);
            }
            if (parametrit.Count > 0)
            {
                return "?" + string.Join('&', parametrit);
            }
            else
            {
                return "";
            }
        }
    }
}

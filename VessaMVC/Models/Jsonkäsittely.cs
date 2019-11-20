using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public List<Wct> Lahimmat(decimal lat, decimal lon)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url + "Lahimmat/" + decimal.Round(lat, 8) + "/" + decimal.Round(lon, 8)).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Wct>>(json);
            }
        }

        public List<Wct> Lahimmat(string paikka)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url + "Lahimmat/" + HttpUtility.UrlEncode(paikka)).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Wct>>(json);
            }
        }
    }
}

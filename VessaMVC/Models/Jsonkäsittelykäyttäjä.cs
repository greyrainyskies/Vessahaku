using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace VessaMVC.Models
{
    public class Jsonkäsittelykäyttäjä
    {
        const string url ="https://localhost:44330/api/käyttäjä/";
        public string Jsonhommatkäyttäjä(string nimi = null, int id = -1, string urlinloppu=null)
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
    }
}

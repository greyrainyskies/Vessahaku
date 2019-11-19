using System;
using System.Collections.Generic;


namespace VessaMVC.Models
{
    public class Wct
    {
        

        public int WcId { get; set; }
        public string Nimi { get; set; }
        public string Kaupunki { get; set; }
        public string Katuosoite { get; set; }
        public string Postinro { get; set; }
        public bool? Unisex { get; set; }
        public bool? Saavutettava { get; set; }
        public bool Ilmainen { get; set; }
        public string Aukioloajat { get; set; }
        public string Koodi { get; set; }
        public string Ohjeet { get; set; }
        public DateTime Lisätty { get; set; }
        public DateTime? Muokattu { get; set; }
        public int? KäyttäjäId { get; set; }
       

    }
}

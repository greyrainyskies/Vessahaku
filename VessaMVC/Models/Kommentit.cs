using System;
using System.Collections.Generic;

namespace VessaMVC.Models
{
    public class Kommentit
    {
        public int KommenttiId { get; set; }
        public DateTime Lisätty { get; set; }
        public int Arvio { get; set; }
        public string Sisältö { get; set; }
        public int WcId { get; set; }
        public int? KäyttäjäId { get; set; }

        
    }
}

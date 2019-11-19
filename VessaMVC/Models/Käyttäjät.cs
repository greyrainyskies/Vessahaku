using System;
using System.Collections.Generic;

namespace VessaMVC.Models
{
    public class Käyttäjät
    {
        public int KäyttäjäId { get; set; }
        public string Nimimerkki { get; set; }
        public string Salasana { get; set; }
        public string Sähköposti { get; set; }
    }
}

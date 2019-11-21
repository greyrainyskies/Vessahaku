using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VessaMVC.Models
{
    public class Results
    {
        public string Osoite { get; set; }
        public List<Wct> Vessat { get; set; }
    }
}

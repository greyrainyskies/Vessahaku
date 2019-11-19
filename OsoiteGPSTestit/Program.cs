using System;
using OsoiteGPS;

namespace OsoiteGPSTestit
{
    class Program
    {
        static void Main(string[] args)
        {
            //Osoite.Haku("Kamppi");
            Console.WriteLine(Osoite.Haku("Jarrumiehenkatu 2, 00520 Pasila"));
        }
    }
}

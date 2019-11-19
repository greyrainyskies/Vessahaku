using System;
using OsoiteGPS;

namespace OsoiteGPSTestit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var piste = Osoite.Haku("Kiasma");
                Console.WriteLine(piste.Coordinates[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

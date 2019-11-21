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
                var postinumero = Osoite.SijainninPerusteella(60.2017793M, 24.9377817M);
                Console.WriteLine(postinumero);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

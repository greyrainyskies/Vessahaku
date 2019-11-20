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
                var postinumero = Osoite.Postinumero("XX", "");
                Console.WriteLine(postinumero);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

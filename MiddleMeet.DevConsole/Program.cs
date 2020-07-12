using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiddleMeet.Domain;
using MiddleMeet.GoogleSDK.Client;
using MiddleMeet.GoogleSDK.Geocode;
using MiddleMeet.GoogleSDK.Place;

namespace MiddleMeet.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleApiClient client = new GoogleApiClient(new GoogleApiConfig(){ApiKey="AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY"});
            IGeocodeApi geocodeApi = new GeocodeApi(client);
            
            List<string> addresses = new List<string>();

            Console.WriteLine("Please enter the addresses in interest (At least 2. Type done after your last address.):");
            string address;
            Console.WriteLine($"Address #{addresses.Count() + 1}:");
            while ((address = Console.ReadLine()) != "done")
            {
                addresses.Add(address);
                Console.WriteLine($"Address #{addresses.Count() + 1}:");
            }

            Console.WriteLine("How much of a radius around the middle would you like to search? (Enter a number in miles)");
            string radiusRaw = Console.ReadLine();
            double radius = double.Parse(radiusRaw);

            Console.WriteLine("Beep boop bop bop... Computing...");

            GetPlacesAroundMidPointOfLocationsQuery command = new GetPlacesAroundMidPointOfLocationsQuery(
                geocodeApi,
                new PlaceApi(client),
                addresses,
                15);
            
            IEnumerable<Location> locations = command.Run().Result;

            Console.WriteLine("\n\n\nSome places you might look into are:");
            Console.WriteLine(string.Join("\n", locations.Select(l => l.ToString())));
        }
    }
}

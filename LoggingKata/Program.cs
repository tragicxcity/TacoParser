using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------
            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");
            logger.LogError($"Lines: {lines[0]}");
            logger.LogWarning($"lines: {lines[1]}");
            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();
            ITrackable tacoBell1 = new TacoBell();
            ITrackable tacoBell2 = new TacoBell();
            double distance = 0;
            
            var Geo = new GeoCoordinate();
            var Geo1 = new GeoCoordinate();
            // DON'T FORGET TO LOG YOUR STEPS
            // Now that your Parse method is completed, START BELOW ----------
            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            foreach (var locA in locations)
            {
                Geo.Longitude = locA.Location.Longitude;
                Geo.Latitude = locA.Location.Latitude;
                foreach (var locB in locations)
                { 
                    Geo1.Longitude = locB.Location.Longitude;
                    Geo1.Latitude = locB.Location.Latitude;
                    Geo.GetDistanceTo(Geo1);
                    
                    if (Geo.GetDistanceTo(Geo1) > distance)
                    {
                        distance = Geo.GetDistanceTo(Geo1);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
                
            }
            logger.LogInfo($"{tacoBell1.Name}");
            logger.LogInfo($"{tacoBell2.Name}");
            Console.WriteLine($"distance converted to miles is {distance * 0.000621371192}.");

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.



        }
    }
}

using SmartCab.Core.Interfaces;
using SmartCab.Core.Models;
using SmartCab.Core.Services;

namespace SmartCab.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowWelcomeMessage();

            // Seed Cabs
            var cabRepo = new InMemoryCabRepository();
            var cabs = new List<Cab>
            {
                new Cab("CAB1", new Location(0, 0)),
                new Cab("CAB2", new Location(0, 0)),
                new Cab("CAB3", new Location(0, 0)),
                new Cab("CAB4", new Location(0, 0)),
                new Cab("CAB5", new Location(0, 0))
            };
            cabRepo.SeedInitialCabs(cabs);

            var fareCalculator = new FareCalculator();
            var dispatchService = new DispatchService(cabRepo, fareCalculator);

            // Ride history map: CabId → List of rides
            var rideHistory = new Dictionary<string, List<(Location pickup, Location dropoff, decimal fare)>>();

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Book a Ride");
                Console.WriteLine("2. Check Cab Status");
                Console.WriteLine("3. View Cab Ride History");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var pickup = ReadLocation("pickup");
                        var dropoff = ReadLocation("drop-off");
                        var request = new RideRequest(pickup, dropoff);

                        var assignedCab = dispatchService.AssignCab(request);
                        decimal fare = fareCalculator.CalculateFare(pickup, dropoff);
                        if (assignedCab != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nCab {assignedCab.Id} booked.");
                            Console.WriteLine($" Cab moved to pickup: ({pickup.X},{pickup.Y})");
                            Console.WriteLine($" Status: {assignedCab.Status}");
                            Console.WriteLine($" Fare: Rs {fare}");
                            Console.ResetColor();

                            Thread.Sleep(1000); // simulate time

                            dispatchService.CompleteRide(assignedCab, request);

                          
                            Console.WriteLine($"\n Ride completed.");
                            Console.WriteLine($" Drop Location: ({dropoff.X},{dropoff.Y})");
                           

                            // Save ride history
                            if (!rideHistory.ContainsKey(assignedCab.Id))
                                rideHistory[assignedCab.Id] = new List<(Location, Location, decimal)>();

                            rideHistory[assignedCab.Id].Add((pickup, dropoff, fare));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ No cabs available.");
                            Console.ResetColor();
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nAvailable Cabs:");
                        foreach (var c in cabRepo.GetAllCabs())
                        {
                            Console.WriteLine($"- {c.Id}");
                        }

                        Console.Write("Enter Cab ID to check status: ");
                        var cabId = Console.ReadLine()?.Trim().ToUpper();
                        var cab = cabRepo.GetCabById(cabId ?? "");

                        if (cab != null)
                        {
                            Console.WriteLine($"\nCab {cab.Id} is {cab.Status}");
                            Console.WriteLine($" Location: ({cab.CurrentLocation.X},{cab.CurrentLocation.Y})");
                            Console.WriteLine($" Total Earnings: Rs {cab.TotalEarnings}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Cab not found.");
                            Console.ResetColor();
                        }
                        break;


                    case "3":
                        Console.WriteLine("\nCabs with ride history:");
                        if (rideHistory.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" No rides have been completed yet.");
                            Console.ResetColor();
                            break;
                        }

                        foreach (var cabIdKey in rideHistory.Keys)
                        {
                            Console.WriteLine($"- {cabIdKey}");
                        }

                        Console.Write("Enter Cab ID to view history: ");
                        var historyId = Console.ReadLine()?.Trim().ToUpper();

                        if (historyId != null && rideHistory.ContainsKey(historyId))
                        {
                            Console.WriteLine($"\n Ride History for {historyId}:");
                            Console.WriteLine("------------------------------------------");

                            int rideNumber = 1;
                            foreach (var ride in rideHistory[historyId])
                            {
                                Console.WriteLine($" Ride #{rideNumber++}");
                                Console.WriteLine($" Pickup Location : ({ride.pickup.X}, {ride.pickup.Y})");
                                Console.WriteLine($" Drop Location   : ({ride.dropoff.X}, {ride.dropoff.Y})");
                                Console.WriteLine($" Fare            : Rs {ride.fare}");
                                Console.WriteLine("------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" No history found for this cab.");
                            Console.ResetColor();
                        }
                        break;


                    case "4":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(" Exiting... Thank you for using SmartCab!");
                        Console.ResetColor();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Invalid choice. Try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine(" Welcome to SmartCab Dispatch Simulator! 🚕");
            Console.WriteLine("===============================================");
            Console.ResetColor();
            Console.WriteLine("This simulation helps you test cab dispatching logic.");
            Console.WriteLine("Enter pickup and drop-off points on an open grid (X ≥ 0, Y ≥ 0).");
            Console.WriteLine("Cabs will be assigned based on nearest availability.");
            Console.WriteLine("-------------------------------------------------------\n");
            Console.ResetColor();
        }

        static Location ReadLocation(string type)
        {
            while (true)
            {
                Console.Write($"Enter {type} location (X Y): ");
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int x) &&
                        int.TryParse(parts[1], out int y))
                    {
                        return new Location(x, y);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Invalid input. Please enter two integers separated by space.");
                Console.ResetColor();
            }
        }
    }
}

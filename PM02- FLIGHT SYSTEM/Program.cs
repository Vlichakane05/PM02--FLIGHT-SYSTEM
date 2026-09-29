using System;

namespace FlightBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Flight Booking System";

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("       FLIGHT BOOKING SYSTEM");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("1. View Flights");
                Console.WriteLine("2. Search Flights");
                Console.WriteLine("3. Make Booking");
                Console.WriteLine("4. View Booking");
                Console.WriteLine("5. Cancel Booking");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nView Flights selected.");
                        break;

                    case "2":
                        Console.WriteLine("\nSearch Flights selected.");
                        break;

                    case "3":
                        Console.WriteLine("\nMake Booking selected.");
                        break;

                    case "4":
                        Console.WriteLine("\nView Booking selected.");
                        break;

                    case "5":
                        Console.WriteLine("\nCancel Booking selected.");
                        break;

                    case "6":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nThank you for using the Flight Booking System.");
        }
    }
}
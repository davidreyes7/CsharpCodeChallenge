using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        // Configure database connection
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TVShowContext>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

        // Create the context, ensure DB is created and Table exists
        using (var context = new TVShowContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();

            // Create the TVShowRepository and TVShowService
            var repository = new TVShowRepository(context);
            var service = new TVShowService(repository);

            string command = "";

            while (command != "exit")
            {
                Console.Clear();
                Console.WriteLine("Instructions:");
                Console.WriteLine("1. Write 'list' to print all available TV Shows.");
                Console.WriteLine("2. Write 'favorites' to list all TV Shows marked as favorites.");
                Console.WriteLine("3. Write the ID of a TV Show to toggle as favorite/non-favorite.");
                Console.WriteLine("4. Write 'exit' to stop the program");
                Console.WriteLine("Enter a command: ");

                command = Console.ReadLine();

                // Valid input
                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine("Command cannot be empty. Please try again.");
                    Console.ReadLine();
                    continue;
                }

                // Possible inputs
                if (command == "exit")
                {
                    Environment.Exit(0);
                }
                else if (command == "list")
                {
                    var shows = service.ListShows();
                    PrintShows(shows);
                }
                else if (command == "favorites")
                {
                    var favoriteShows = service.ListFavorites();
                    PrintShows(favoriteShows);
                }
                else if (int.TryParse(command, out int id))
                {
                    try
                    {
                        service.ToggleFavorite(id);
                        Console.WriteLine($"Toggled favorite status for TV show with ID {id}.");
                        Console.ReadLine();
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                // Input error
                else
                {
                    Console.WriteLine("Command not recognized. Please try again");
                    Console.ReadLine();
                }
            }
        }
    }

    static void PrintShows(IEnumerable<TVShow> shows)
    {
        if (!shows.Any())
        {
            Console.WriteLine("No TV shows found.");
        }
        else
        {
            foreach (var show in shows)
            {
                var star = show.Favorite ? "*" : "";
                Console.WriteLine($"{show.Id}: {show.Title} {star}");
            }
        }
        Console.ReadLine();
    }
}

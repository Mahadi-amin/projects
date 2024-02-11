using FinalProject.Database;
using FinalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CageAquarium
{
    public class CageOrAquariumManagement
    {
        MyDbContext context = new MyDbContext();
        public void ShowCagesAndAquariums()
        {
            var allCagesAndAquariums = context.CagesOrAquariums.ToList();

            if (allCagesAndAquariums.Any())
            {
                Console.WriteLine("Cages and Aquariums :");

                Console.WriteLine("--------------------------------");
                Console.WriteLine("| ID    | Type                 |");
                Console.WriteLine("--------------------------------");

                foreach (var cageOrAquarium in allCagesAndAquariums)
                {
                    Console.WriteLine($"| {cageOrAquarium.CageOrAquariumId,-5} | {cageOrAquarium.Type,-20} |");
                }
                Console.WriteLine("--------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The cages and aquariums is empty.");
                Console.ResetColor();
            }
        }
        public void AddCageOrAquarium()
        {
            while (true)
            {
                Console.WriteLine("Enter Cage or Aquarium type");
                var type = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(type))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Type cannot be empty. Please enter a valid type.");
                    Console.ResetColor();
                    continue;
                }

                var newCageOrAquarium = new CageOrAquarium
                {
                    Type = type
                };

                context.Add(newCageOrAquarium);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cage or Aquarium added successfully.");
                Console.ResetColor();
                break;
            }
        }
        public void DeleteAddCageOrAquarium()
        {
            bool isValidId = false;

            while (!isValidId)
            {
                Console.WriteLine("Enter Cage or Aquarium ID to Delete");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid Cage or Aquarium ID.");
                    Console.ResetColor();
                    continue;
                }

                var cageOrAquariumToDelete = context.CagesOrAquariums.FirstOrDefault(c => c.CageOrAquariumId == id);

                if (cageOrAquariumToDelete != null)
                {
                    var petsInCageOrAquarium = context.Pets.Any(p => p.CageOrAquariumId == id);

                    if (petsInCageOrAquarium)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This Cage or Aquarium contains pets and cannot be deleted.");
                        Console.ResetColor();
                        break;
                    }

                    isValidId = true;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Are you sure you want to delete this Cage or Aquarium? (Yes/No)");
                    Console.ResetColor();

                    string confirmation = Console.ReadLine()?.ToLower();

                    if (confirmation != null && (confirmation == "yes" || confirmation == "y"))
                    {
                        context.CagesOrAquariums.Remove(cageOrAquariumToDelete);
                        context.SaveChanges();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Cage or Aquarium deleted successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cage or Aquarium not found with the given ID. Retry deletion?");
                    Console.ResetColor();

                    Console.WriteLine("Yes/No");
                    string response = Console.ReadLine()?.ToLower();

                    if (response != null && (response == "yes" || response == "y"))
                    {
                        continue;
                    }
                    else
                    {
                        isValidId = true;
                    }
                }
            }
        }
    }
}

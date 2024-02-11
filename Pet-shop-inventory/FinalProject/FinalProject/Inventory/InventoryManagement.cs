using FinalProject.Database;
using FinalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Inventory
{
    public class InventoryManagement
    {
        private MyDbContext context = new MyDbContext();
        public void ShowInventory()
        {
            var allPets = context.Pets.ToList();

            if (allPets.Any())
            {
                Console.WriteLine("Inventory:");

                Console.WriteLine("-----------------------------------------------------------------------------");
                Console.WriteLine("| ID    | Name                 | Type       | Price      | Cage/Aquarium Id |");
                Console.WriteLine("-----------------------------------------------------------------------------");

                foreach (var pet in allPets)
                {
                    Console.WriteLine($"| {pet.Id,-5} | {pet.Name,-20} | {pet.Type,-10} | {pet.Price,-10} | {pet.CageOrAquariumId,-16} |");
                }

                Console.WriteLine("-----------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The inventory is empty.");
                Console.ResetColor();
            }
        }
        public void AddToInventory()
        {
            if (!context.CagesOrAquariums.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no cage or Aquarium available. Please add a cage or Aquarium first.");
                Console.ResetColor();
                return; 
            }
            string name = "";
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Enter pet Name");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pet name cannot be empty. Please enter a valid name.");
                    Console.ResetColor();
                }
            }

            string type = "";
            while (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Enter pet type");
                type = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(type))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pet type cannot be empty. Please enter a valid type.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Enter purchase price");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid price. Please enter a valid numeric value.");
                Console.ResetColor();
            }

            int cageOrAquariumId = 0;
            bool cageOrAquariumExists = false;

            while (!cageOrAquariumExists)
            {
                Console.WriteLine("Enter Cage Or AquariumId");
                while (!int.TryParse(Console.ReadLine(), out cageOrAquariumId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Cage or Aquarium ID. Please enter a valid integer value.");
                    Console.ResetColor();
                }

                cageOrAquariumExists = CheckCageOrAquariumExistence(cageOrAquariumId);

                if (!cageOrAquariumExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cage or Aquarium ID does not exist in the database. Please enter an existing Cage or Aquarium ID.");
                    Console.ResetColor();
                }
            }

            var newPet = new Pet
            {
                Name = name,
                Type = type,
                Price = price,
                CageOrAquariumId = cageOrAquariumId
            };

            context.Add(newPet);
            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pet added successfully.");
            Console.ResetColor();
        }
        private bool CheckCageOrAquariumExistence(int cageOrAquariumId)
        {
            var cageOrAquarium = context.CagesOrAquariums.FirstOrDefault(c => c.CageOrAquariumId == cageOrAquariumId);
            return cageOrAquarium != null;
        }
        public void UpdateInventory()
        {
            bool isValidId = false;
            if (!context.Pets.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no pets available. Please add a pet first.");
                Console.ResetColor();
                return;
            }

            while (!isValidId)
            {
                Console.WriteLine("Enter Pet ID to Update pet");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid Pet ID.");
                    Console.ResetColor();
                    continue;
                }

                var petToUpdate = context.Pets.FirstOrDefault(p => p.Id == id);

                if (petToUpdate != null)
                {
                    bool isValidUpdate = false;

                    while (!isValidUpdate)
                    {
                        string name = "";
                        while (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Enter pet Name to Update");
                            name = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Pet name cannot be empty. Please enter a valid name.");
                                Console.ResetColor();
                            }
                        }

                        string type = "";
                        while (string.IsNullOrWhiteSpace(type))
                        {
                            Console.WriteLine("Enter pet type to Update");
                            type = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(type))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Pet type cannot be empty. Please enter a valid type.");
                                Console.ResetColor();
                            }
                        }

                        double price;
                        Console.WriteLine("Enter purchase price to Update");
                        while (!double.TryParse(Console.ReadLine(), out price))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid price. Please enter a valid numeric value.");
                            Console.ResetColor();
                        }

                        int cageOrAquariumId = 0;
                        bool cageOrAquariumExists = false;

                        while (!cageOrAquariumExists)
                        {
                            Console.WriteLine("Enter Cage Or AquariumId to Update");
                            while (!int.TryParse(Console.ReadLine(), out cageOrAquariumId))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Cage or Aquarium ID. Please enter a valid integer value.");
                                Console.ResetColor();
                            }

                            cageOrAquariumExists = CheckCageOrAquariumExistence(cageOrAquariumId);

                            if (!cageOrAquariumExists)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Cage or Aquarium ID does not exist in the database. Please enter an existing Cage or Aquarium ID.");
                                Console.ResetColor();
                            }
                        }

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || double.IsNaN(price))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Name, type, or price cannot be empty. Please provide valid inputs.");
                            Console.ResetColor();
                        }
                        else
                        {
                            isValidUpdate = true;

                            petToUpdate.Name = name;
                            petToUpdate.Type = type;
                            petToUpdate.Price = price;
                            petToUpdate.CageOrAquariumId = cageOrAquariumId;

                            context.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Pet updated successfully.");
                            Console.ResetColor();
                        }
                    }

                    isValidId = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pet not found with the given ID. Retry update?");
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
        public void DeleteFromInventory()
        {
            bool isValidId = false;
            if (!context.Pets.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no pets available.");
                Console.ResetColor();
                return;
            }

            while (!isValidId)
            {
                Console.WriteLine("Enter Pet ID to Delete pet");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid Pet ID.");
                    Console.ResetColor();
                    continue;
                }

                var petToDelete = context.Pets.FirstOrDefault(p => p.Id == id);

                if (petToDelete != null)
                {
                    isValidId = true;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Are you sure you want to delete this pet? (Yes/No)");
                    Console.ResetColor();

                    string confirmation = Console.ReadLine()?.ToLower();

                    if (confirmation != null && (confirmation == "yes" || confirmation == "y"))
                    {
                        context.Pets.Remove(petToDelete);
                        context.SaveChanges();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Pet deleted successfully.");
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
                    Console.WriteLine("Pet not found with the given ID. Retry deletion?");
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

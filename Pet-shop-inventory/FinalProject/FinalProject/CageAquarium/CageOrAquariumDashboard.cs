using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CageAquarium
{
    public class CageOrAquariumDashboard
    {
        public void CageOrAquariumDetails()
        {
            var cageOrAquariumManagement = new CageOrAquariumManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Cage Or Aquarium Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. Show all Cage Or Aquarium");
                Console.WriteLine("2. Add a Cage Or Aquarium");
                Console.WriteLine("3. Delete Pet from Inventory");
                Console.WriteLine("0. Back to Main Menu");

                if (invalidInput)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    invalidInput = false;
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    Console.Clear();

                    switch (choice)
                    {
                        case 0:
                            exit = true;
                            break;
                        case 1:
                            cageOrAquariumManagement.ShowCagesAndAquariums();
                            break;
                        case 2:
                            cageOrAquariumManagement.AddCageOrAquarium();
                            break;
                        case 3:
                            cageOrAquariumManagement.DeleteAddCageOrAquarium();
                            break;
                        default:
                            invalidInput = true;
                            break;
                    }
                }
                else
                {
                    invalidInput = true;
                }
            }

            string[] animationFrames = { "|", "/", "-", "\\" };
            int counter = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\r" + "Exiting CageOrAquarium Dashboard... " + animationFrames[counter]);
                Thread.Sleep(100); 
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

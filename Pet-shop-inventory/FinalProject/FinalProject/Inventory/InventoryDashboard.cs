using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Inventory
{
    public class InventoryDashboard
    {
        public void InventoryDetails()
        {
            InventoryManagement inventory = new InventoryManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Inventory Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. Show Inventory");
                Console.WriteLine("2. Add Pet to Inventory");
                Console.WriteLine("3. Update Pet in Inventory");
                Console.WriteLine("4. Delete Pet from Inventory");
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
                            inventory.ShowInventory();
                            break;
                        case 2:
                            inventory.AddToInventory();
                            break;
                        case 3:
                            inventory.UpdateInventory();
                            break;
                        case 4:
                            inventory.DeleteFromInventory();
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
                Console.Write("\r" + "Exiting Inventory Dashboard... " + animationFrames[counter]);
                Thread.Sleep(100);
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

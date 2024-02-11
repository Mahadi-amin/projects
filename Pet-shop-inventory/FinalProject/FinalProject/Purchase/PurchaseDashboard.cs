using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Purchase
{
    public class PurchaseDashboard
    {
        public void PurchsaeInformationDetails()
        {
            PurchaseManagement purchaseManagement = new PurchaseManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Purchase Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. Show Purchase history");
                Console.WriteLine("2. Add Purchase Informetion");
                Console.WriteLine("3. Last 30 days Purchase Informetion");
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
                            purchaseManagement.ShowPurchaseInformationWithinDates();
                            break;
                        case 2:
                            purchaseManagement.AddToInventory();
                            break;
                        case 3:
                            purchaseManagement.ShowLast30DaysPurchaseInformation();
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
                Console.Write("\r" + "Exiting Purchase Dashboard... " + animationFrames[counter]);
                Thread.Sleep(100);
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

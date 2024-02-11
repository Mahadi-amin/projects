using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Sales
{
    public class SalesRecordDashboard
    {
        public void SalesRecordDetails()
        {
            SalesRecordManagement salesRecordManagement = new SalesRecordManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Sales Record Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. Show Sales history");
                Console.WriteLine("2. Add Sales Informetion");
                Console.WriteLine("3. Last 30 days Sales Informetion");
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
                            salesRecordManagement.ShowSalesInformationWithinDates();
                            break;
                        case 2:
                            salesRecordManagement.AddToSales();
                            break;
                        case 3:
                            salesRecordManagement.ShowLast30DaysSalesInformation();
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
                Console.Write("\r" + "Exiting SalesRecord Dashboard... " + animationFrames[counter]);
                Thread.Sleep(100);
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

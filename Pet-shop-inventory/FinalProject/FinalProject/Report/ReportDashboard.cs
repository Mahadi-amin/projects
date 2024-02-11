using FinalProject.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Report
{
    public class ReportDashboard
    {
        public void ReportDetails()
        {
            SalesRecordManagement salesRecordManagement = new SalesRecordManagement();
            ReportManagement reportManagement = new ReportManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Record Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. Monthly sales report");
                Console.WriteLine("2. Monthly Purchase report");
                Console.WriteLine("3. Monthly sales and purchase report");
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
                            reportManagement.ShowMonthlySalesReport();
                            break;
                        case 2:
                            reportManagement.ShowMonthlyPurchaseReport();
                            break;
                        case 3:
                            reportManagement.ShowMonthlySalesAndPurchaseReport();
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
                Console.Write("\r" + "Exiting Report Details... " + animationFrames[counter]);
                Thread.Sleep(100);
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

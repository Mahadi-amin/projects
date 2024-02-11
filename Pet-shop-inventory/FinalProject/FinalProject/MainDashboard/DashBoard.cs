using FinalProject.CageAquarium;
using FinalProject.Inventory;
using FinalProject.Login;
using FinalProject.Purchase;
using FinalProject.Report;
using FinalProject.Sales;
using FinalProject.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.MainDashboard
{
    public class DashBoard
    {
        public void DisplayMenu()
        {
            InventoryDashboard inventoryDashboard = new InventoryDashboard();
            ScheduleDashboard schedule = new ScheduleDashboard();
            CageOrAquariumDashboard cageOrAquariumDashboard = new CageOrAquariumDashboard();
            PurchaseDashboard purchaseDashboard = new PurchaseDashboard();
            SalesRecordDashboard salesRecordDashboard = new SalesRecordDashboard();
            ReportDashboard reportDashboard = new ReportDashboard();
            LoginAuth loginAuth = new LoginAuth();

            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Menu ---");
                Console.ResetColor();
                Console.WriteLine("1. Inventory Details");
                Console.WriteLine("2. Feeding Schedules Details");
                Console.WriteLine("3. Cage or Aquarium Details");
                Console.WriteLine("4. Purchaase Details");
                Console.WriteLine("5. Sales Details");
                Console.WriteLine("6. Reports");
                Console.WriteLine("7. Change Password");
                Console.WriteLine("8. Logout");


                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            inventoryDashboard.InventoryDetails();
                            break;
                        case 2:
                            schedule.ScheduleDetails();
                            break;
                        case 3:
                            cageOrAquariumDashboard.CageOrAquariumDetails();
                            break;
                        case 4:
                            purchaseDashboard.PurchsaeInformationDetails();
                            break;
                        case 5:
                            salesRecordDashboard.SalesRecordDetails();
                            break;
                        case 6:
                            reportDashboard.ReportDetails();
                            break;
                        case 7:
                            loginAuth.ChangePassword();
                            break;
                        case 8:
                            exit = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                }
            }
        }
    }
}

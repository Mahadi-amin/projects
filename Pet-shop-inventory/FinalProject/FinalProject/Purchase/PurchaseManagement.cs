using FinalProject.Database;
using FinalProject.Entities;
using FinalProject.ValidationForDate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Purchase
{
    public class PurchaseManagement
    {
        private MyDbContext context = new MyDbContext();
        DateValidation dateValidation = new DateValidation();
        public void ShowPurchaseInformationWithinDates()
        {
            string[] allowedFormats = { "dd-MM-yyyy", "dd.MM.yyyy", "ddMMyyyy" };

            DateTime startDate = dateValidation.GetValidatedDateInput("Enter start date (DD-MM-YYYY):", allowedFormats);
            DateTime endDate = dateValidation.GetValidatedDateInput("Enter end date (DD-MM-YYYY):", allowedFormats);

            DateTime formattedStartDate = DateTime.ParseExact(startDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime formattedEndDate = DateTime.ParseExact(endDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            while (formattedStartDate > formattedEndDate)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Start date cannot be greater than the end date. Please re-enter dates.");
                Console.ResetColor();

                startDate = dateValidation.GetValidatedDateInput("Enter start date (DD-MM-YYYY):", allowedFormats);
                endDate = dateValidation.GetValidatedDateInput("Enter end date (DD-MM-YYYY,):", allowedFormats);

                formattedStartDate = DateTime.ParseExact(startDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                formattedEndDate = DateTime.ParseExact(endDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            var purchasesWithinDates = context.PurchaseInformations
                .Where(p => p.PurchaseDate >= formattedStartDate && p.PurchaseDate <= formattedEndDate)
                .ToList();

            if (purchasesWithinDates.Any())
            {
                Console.WriteLine($"Purchase Information between {formattedStartDate.ToString("dd-MM-yyyy")} and {formattedEndDate.ToString("dd-MM-yyyy")}");

                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("| ID    | Seller Name           | Contact Info      | Type of Pet | Purchase Date |");
                Console.WriteLine("-----------------------------------------------------------------------------------");

                foreach (var purchase in purchasesWithinDates)
                {
                    Console.WriteLine($"| {purchase.PurchaseId,-5} | {purchase.SellerName,-21} | {purchase.SellerContactInfo,-17} | {purchase.TypeOfPet,-11} | {purchase.PurchaseDate.ToString("dd-MM-yyyy"),-13} |");
                }
                Console.WriteLine("-----------------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No purchase information available within the specified dates.");
                Console.ResetColor();
            }
        }
        public void AddToInventory()
        {
            string sellerName, sellerContactInfo, typeOfPet;
            DateTime purchaseDate;
            double purchasePrice;

            do
            {
                Console.WriteLine("Enter seller name:");
                sellerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sellerName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Seller name cannot be empty. Please enter a valid name.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(sellerName));

            do
            {
                Console.WriteLine("Enter seller contact info:");
                sellerContactInfo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sellerContactInfo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Seller contact info cannot be empty. Please enter valid information.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(sellerContactInfo));

            do
            {
                Console.WriteLine("Enter type of pet:");
                typeOfPet = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(typeOfPet))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Type of pet cannot be empty. Please enter a valid type.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(typeOfPet));

            string[] dateFormats = { "dd-MM-yyyy", "dd.MM.yyyy", "ddMMyyyy" };
            string enteredDate;

            do
            {
                Console.WriteLine("Enter purchase date (DD-MM-YYYY):");
                enteredDate = Console.ReadLine();
            } while (!DateTime.TryParseExact(enteredDate, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate));

            while (purchaseDate > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Purchase date is in the future. Please enter a date up to today's date.");
                Console.ResetColor();

                do
                {
                    Console.WriteLine("Enter purchase date (DD-MM-YYYY):");
                    enteredDate = Console.ReadLine();
                } while (!DateTime.TryParseExact(enteredDate, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate));
            }

            do
            {
                Console.WriteLine("Enter purchase price:");
            } while (!double.TryParse(Console.ReadLine(), out purchasePrice));

            while (purchasePrice <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Purchase price must be a positive number. Please enter a valid price.");
                Console.ResetColor();

                do
                {
                    Console.WriteLine("Enter purchase price:");
                } while (!double.TryParse(Console.ReadLine(), out purchasePrice));
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning: Once added, this information cannot be modified. Confirm adding purchase information? (Y/N)");
            Console.ResetColor();

            var confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                var newPurchase = new PurchaseInformation
                {
                    SellerName = sellerName,
                    SellerContactInfo = sellerContactInfo,
                    TypeOfPet = typeOfPet,
                    PurchaseDate = purchaseDate,
                    PurchasePrice = purchasePrice
                };

                context.PurchaseInformations.Add(newPurchase);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Purchase information added successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Purchase information not added. Operation cancelled.");
                Console.ResetColor();
            }
        }
        public void ShowLast30DaysPurchaseInformation()
        {
            DateTime today = DateTime.Now;
            DateTime startDate = today.AddDays(-30); 

            var purchasesLast30Days = context.PurchaseInformations
                .Where(p => p.PurchaseDate >= startDate && p.PurchaseDate <= today)
                .ToList();

            if (purchasesLast30Days.Any())
            {
                Console.WriteLine($"Purchase Information for the Last 30 Days (from {startDate.ToString("dd-MM-yyyy")} to {today.ToString("dd-MM-yyyy")})");

                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("| ID    | Seller Name           | Contact Info      | Type of Pet | Purchase Date |");
                Console.WriteLine("-----------------------------------------------------------------------------------");

                foreach (var purchase in purchasesLast30Days)
                {
                    Console.WriteLine($"| {purchase.PurchaseId,-5} | {purchase.SellerName,-21} | {purchase.SellerContactInfo,-18} | {purchase.TypeOfPet,-11} | {purchase.PurchaseDate.ToString("dd-MM-yyyy"),-13}|");
                }

                Console.WriteLine("-----------------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No purchase information available for the last 30 days.");
                Console.ResetColor ();
            }
        }
    }
}

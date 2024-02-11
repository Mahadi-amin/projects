using FinalProject.Database;
using FinalProject.Entities;
using FinalProject.ValidationForDate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Sales
{
    public class SalesRecordManagement
    {
        MyDbContext context = new MyDbContext();
        DateValidation dateValidation = new DateValidation();
        public void AddToSales()
        {
            string customerName, customerContactInfo, enteredDate, petType;
            double salePrice;
            DateTime saleDate;

            do
            {
                Console.WriteLine("Enter customer name:");
                customerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(customerName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Customer name cannot be empty. Please enter a valid name.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(customerName));

            do
            {
                Console.WriteLine("Enter customer contact info:");
                customerContactInfo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(customerContactInfo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Customer contact info cannot be empty. Please enter valid information.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(customerContactInfo));

            do
            {
                Console.WriteLine("Enter sale price:");
            } while (!double.TryParse(Console.ReadLine(), out salePrice) || salePrice < 0); 

            int petId;
            bool petExists = false;

            do
            {
                Console.WriteLine("Enter pet ID:");
                if (!int.TryParse(Console.ReadLine(), out petId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid pet ID:");
                    Console.ResetColor();
                    continue;
                }

                petExists = context.Pets.Any(p => p.Id == petId);

                if (!petExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Entered Pet ID does not exist.");
                    Console.ResetColor();
                }
            } while (!petExists);

            petType = context.Pets.FirstOrDefault(p => p.Id == petId)?.Type;

            do
            {
                Console.WriteLine("Enter sale date (DD-MM-YYYY):");
                enteredDate = Console.ReadLine();
            } while (!DateTime.TryParseExact(enteredDate, new[] { "dd-MM-yyyy", "dd.MM.yyyy", "ddMMyyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out saleDate));

            while (saleDate > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Warning: Sale date is in the future. Please enter a date up to today's date.");
                Console.ResetColor();

                do
                {
                    Console.WriteLine("Enter sale date (DD-MM-YYYY):");
                    enteredDate = Console.ReadLine();
                } while (!DateTime.TryParseExact(enteredDate, new[] { "dd-MM-yyyy", "dd.MM.yyyy", "ddMMyyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out saleDate));
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning: Once added, this information cannot be modified. Confirm adding sale information? (Y/N)");
            Console.ResetColor();

            var confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                var newSale = new SalesRecord
                {
                    CustomerName = customerName,
                    CustomerContactInfo = customerContactInfo,
                    SalePrice = salePrice,
                    SaleDate = saleDate,
                    PetId = petId,
                    Type = petType ?? ""
                };

                context.SalesRecords.Add(newSale);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sale information added successfully.");
                Console.ResetColor();

                var petToDelete = context.Pets.FirstOrDefault(p => p.Id == petId);
                if (petToDelete != null)
                {
                    context.Pets.Remove(petToDelete);
                    context.SaveChanges();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sale information not added. Operation cancelled.");
                Console.ResetColor();
            }
        }
        public void ShowSalesInformationWithinDates()
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

            var salesWithinDates = context.SalesRecords
                .Where(s => s.SaleDate >= formattedStartDate && s.SaleDate <= formattedEndDate)
                .ToList();

            if (salesWithinDates.Any())
            {
                Console.WriteLine($"Sales Information between {formattedStartDate.ToString("dd-MM-yyyy")} and {formattedEndDate.ToString("dd-MM-yyyy")}");

                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("| ID    | Customer Name         | Contact Info      | Sale Price | Sale Date     |");
                Console.WriteLine("----------------------------------------------------------------------------------");

                foreach (var sale in salesWithinDates)
                {
                    Console.WriteLine($"| {sale.SalesId,-5} | {sale.CustomerName,-21} | {sale.CustomerContactInfo,-17} | {sale.SalePrice,-10} | {sale.SaleDate.ToString("dd-MM-yyyy"),-13} |");
                }

                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No sales information available within the specified dates.");
                Console.ResetColor();
            }
        }
        public void ShowLast30DaysSalesInformation()
        {
            DateTime today = DateTime.Now;
            DateTime startDate = today.AddDays(-30);

            var salesLast30Days = context.SalesRecords
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= today)
                .ToList();

            if (salesLast30Days.Any())
            {
                Console.WriteLine($"Sales Information for the Last 30 Days (from {startDate.ToString("dd-MM-yyyy")} to {today.ToString("dd-MM-yyyy")})");

                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("| ID    | Customer Name         | Contact Info      | Sale Price | Sale Date     |");
                Console.WriteLine("----------------------------------------------------------------------------------");

                foreach (var sale in salesLast30Days)
                {
                    Console.WriteLine($"| {sale.SalesId,-5} | {sale.CustomerName,-21} | {sale.CustomerContactInfo,-17} | {sale.SalePrice,-10} | {sale.SaleDate.ToString("dd-MM-yyyy"),-13} |");
                }

                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No sales information available for the last 30 days.");
                Console.ResetColor();
            }
        }
    }
}

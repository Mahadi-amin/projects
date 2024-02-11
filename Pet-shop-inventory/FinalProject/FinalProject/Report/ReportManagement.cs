
using FinalProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Report
{
    public class ReportManagement
    {
        MyDbContext context = new MyDbContext();
        public void ShowMonthlySalesReport()
        {
            var monthlySales = context.SalesRecords
                .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Sum(s => s.SalePrice)
                })
                .OrderByDescending(g => g.Year)
                .ThenByDescending(g => g.Month)
                .ToList();

            if (monthlySales.Any())
            {
                Console.WriteLine("Monthly Sales Report");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("|   Year   |   Month   |  Total Sales |");
                Console.WriteLine("---------------------------------------");

                foreach (var sale in monthlySales)
                {
                    Console.WriteLine($"|   {sale.Year}   |   {sale.Month}      |   {sale.TotalSales}       |");
                }

                Console.WriteLine("---------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No sales data available.");
                Console.ResetColor();
            }
        }
        public void ShowMonthlyPurchaseReport()
        {
            var monthlyPurchases = context.PurchaseInformations
                .GroupBy(p => new { p.PurchaseDate.Year, p.PurchaseDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalPurchases = g.Sum(p => p.PurchasePrice)
                })
                .OrderByDescending(g => g.Year)
                .ThenByDescending(g => g.Month)
                .ToList();

            if (monthlyPurchases.Any())
            {
                Console.WriteLine("Monthly Purchase Report");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("|   Year   |   Month   |  Total Purchases |");
                Console.WriteLine("-------------------------------------------");

                foreach (var purchase in monthlyPurchases)
                {
                    Console.WriteLine($"|   {purchase.Year}   |   {purchase.Month}      |   {purchase.TotalPurchases}          |");
                }

                Console.WriteLine("-------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No purchase data available.");
                Console.ResetColor();
            }
        }
        public void ShowMonthlySalesAndPurchaseReport()
        {
            int currentYear = DateTime.Now.Year;

            Console.WriteLine("Please enter the year (e.g., 2023):");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > currentYear)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid year:");
                Console.ResetColor();
            }

            Console.WriteLine("Please enter the month (1-12):");
            int month;
            while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid month (1-12):");
                Console.ResetColor();
            }

            var monthlySales = context.SalesRecords
                .Where(s => s.SaleDate.Year == year && s.SaleDate.Month == month)
                .Select(s => new { s.SaleDate, s.SalePrice, s.Type })
                .ToList();

            var monthlyPurchases = context.PurchaseInformations
                .Where(p => p.PurchaseDate.Year == year && p.PurchaseDate.Month == month)
                .Select(p => new { p.PurchaseDate, p.PurchasePrice, p.TypeOfPet })
                .ToList();

            if (monthlySales.Any() || monthlyPurchases.Any())
            {
                Console.WriteLine("Monthly Sales Report");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("| Sales Date       | Price       | Data Type   |");
                Console.WriteLine("------------------------------------------------");

                foreach (var sale in monthlySales)
                {
                    Console.WriteLine($"| {sale.SaleDate.ToShortDateString(),-16} | {sale.SalePrice,-11} | {sale.Type,-11} |");
                }

                Console.WriteLine("------------------------------------------------");

                Console.WriteLine("Monthly Purchase Report");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("| Purchase Date    | Price       | Data Type   |");
                Console.WriteLine("------------------------------------------------");

                foreach (var purchase in monthlyPurchases)
                {
                    Console.WriteLine($"| {purchase.PurchaseDate.ToShortDateString(),-16} | {purchase.PurchasePrice,-11} | {purchase.TypeOfPet,-11} |");
                }

                Console.WriteLine("------------------------------------------------");

                var totalSales = monthlySales.Sum(s => s.SalePrice);
                var totalPurchases = monthlyPurchases.Sum(p => p.PurchasePrice);
                var profitOrLoss = totalSales - totalPurchases;

                if (profitOrLoss >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Profit: {profitOrLoss}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Loss: {Math.Abs(profitOrLoss)}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No sales or purchase data available for the given month and year.");
                Console.ResetColor();
            }
        }
    }
}

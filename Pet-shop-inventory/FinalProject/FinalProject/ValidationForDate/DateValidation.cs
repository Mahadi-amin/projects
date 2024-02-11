using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ValidationForDate
{
    public class DateValidation
    {
        public DateTime GetValidatedDateInput(string prompt, string[] dateFormats, DateTime? maxDate = null)
        {
            DateTime date;
            bool isValidDate = false;

            do
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date) &&
                    (!maxDate.HasValue || date <= maxDate.Value) && date <= DateTime.Now)
                {
                    isValidDate = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input or date exceeds maximum allowed value or is greater than today's date.\nPlease enter a valid date in the specified format.");
                    Console.ResetColor();
                }

            } while (!isValidDate);

            return date;
        }
    }
}

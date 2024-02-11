using FinalProject.Database;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Schedule
{
    public class ScheduleManagement
    {
        MyDbContext _context = new MyDbContext();
        public void ShowAllSchedule()
        {
            var schedulesByCage = _context.FeedingSchedules
                .Include(fs => fs.CageOrAquarium)
                .GroupBy(fs => fs.CageOrAquariumId);

            bool hasAnySchedule = false; 

            foreach (var scheduleGroup in schedulesByCage)
            {
                var cageId = scheduleGroup.Key;

                if (cageId != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Cage ID: {cageId}");
                    Console.WriteLine("----------------------");
                    Console.ResetColor();

                    foreach (var schedule in scheduleGroup)
                    {
                        Console.WriteLine($"Feed Time: {schedule.FeedTime}");
                        hasAnySchedule = true; 
                    }

                    Console.WriteLine();
                }
            }

            if (!hasAnySchedule)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No feeding schedules found.");
                Console.ResetColor ();
            }
        }
        public void ShowSchedule()
        {
            if (!_context.CagesOrAquariums.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no cage or Aquarium available. Please add a cage or Aquarium first.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Enter Cage Id No:");
            if (!int.TryParse(Console.ReadLine(), out int cageId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input for Cage Id. Please enter a valid integer.");
                Console.ResetColor();
                return;
            }

            var cageSchedules = _context.CagesOrAquariums
                .Include(c => c.FeedingSchedules)
                .FirstOrDefault(c => c.CageOrAquariumId == cageId);

            if (cageSchedules != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Feeding Schedules for Cage {cageId}:");
                Console.WriteLine("-----------------------------");
                Console.ResetColor();

                foreach (var schedule in cageSchedules.FeedingSchedules)
                {
                    Console.WriteLine($"Feed Time: {schedule.FeedTime}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No cage found with ID: {cageId}");
                Console.ResetColor();
            }
        }
        public void AddSchedule()
        {
            if (!_context.CagesOrAquariums.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no cage or Aquarium available. Please add a cage or Aquarium first.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Enter Cage Id No:");
            if (!int.TryParse(Console.ReadLine(), out int cageId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input for Cage Id. Please enter a valid integer.");
                Console.ResetColor();
                return;
            }

            var cageExists = _context.CagesOrAquariums.Any(c => c.CageOrAquariumId == cageId);
            if (!cageExists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cage with the provided Id does not exist in the database.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Enter Feeding Schedule:");
            string fSchedule = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fSchedule))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Feeding schedule cannot be empty. Please enter a valid schedule.");
                Console.ResetColor();
                return;
            }

            var newSchedule = new FeedingSchedule
            {
                CageOrAquariumId = cageId,
                FeedTime = fSchedule
            };

            _context.Add(newSchedule);
            _context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Feeding schedule added successfully!");
            Console.ResetColor();
        }
        public void UpdateSchedule()
        {
            if (!_context.FeedingSchedules.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no Feeding Schedules available. Please add a Feeding Schedules first.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Enter the Schedule ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int scheduleId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input for Schedule ID. Please enter a valid integer.");
                Console.ResetColor();
                return;
            }

            var scheduleToUpdate = _context.FeedingSchedules.Find(scheduleId);

            if (scheduleToUpdate != null)
            {
                Console.WriteLine("Enter new Feed Time:");
                string newFeedTime = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newFeedTime))
                {
                    scheduleToUpdate.FeedTime = newFeedTime;
                    _context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Feeding schedule updated successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Feed Time cannot be empty. Update canceled.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No feeding schedule found with ID: {scheduleId}");
                Console.ResetColor();
            }
        }
        public void RemoveSchedule()
        {
            if (!_context.FeedingSchedules.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no Feeding Schedules available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Enter the Schedule ID to remove:");
            if (!int.TryParse(Console.ReadLine(), out int scheduleId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input for Schedule ID. Please enter a valid integer.");
                Console.ResetColor();
                return;
            }

            var scheduleToRemove = _context.FeedingSchedules.Find(scheduleId);

            if (scheduleToRemove != null)
            {
                _context.FeedingSchedules.Remove(scheduleToRemove);
                _context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Feeding schedule removed successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No feeding schedule found with ID: {scheduleId}");
                Console.ResetColor();
            }
        }
    }
}

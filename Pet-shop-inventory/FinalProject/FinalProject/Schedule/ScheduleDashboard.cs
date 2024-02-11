using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Schedule
{
    public class ScheduleDashboard
    {
        public void ScheduleDetails()
        {
            ScheduleManagement schedule = new ScheduleManagement();
            bool exit = false;
            bool invalidInput = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n--- Schedule Management Menu ---");
                Console.ResetColor();

                Console.WriteLine("1. View All Feeding Schedules");
                Console.WriteLine("2. View Individual Cage/Aquarium Feeding Schedules");
                Console.WriteLine("3. Add Feeding Schedule");
                Console.WriteLine("4. Update Feeding Schedule");
                Console.WriteLine("5. Remove Feeding Schedule");
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
                            schedule.ShowAllSchedule();
                            break;
                        case 2:
                            schedule.ShowSchedule();
                            break;
                        case 3:
                            schedule.AddSchedule();
                            break;
                        case 4:
                            schedule.UpdateSchedule();
                            break;
                        case 5:
                            schedule.RemoveSchedule();
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
                Console.Write("\r" + "Exiting Schedule Management... " + animationFrames[counter]);
                Thread.Sleep(100);
                counter = (counter + 1) % animationFrames.Length;
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.Clear();
        }
    }
}

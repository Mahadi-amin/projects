using FinalProject.Database;
using FinalProject.Entities;
using FinalProject.MainDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Login
{
    public class LoginAuth
    {
        MyDbContext dbContext = new MyDbContext();
        DashBoard dashBoard = new DashBoard();
        public void PerformLogin()
        {
            const int maxAttempts = 3;
            int attempts = 0;
            bool isAuthenticated = false;

            do
            {
                string adminName = string.Empty;
                string adminPassword = string.Empty;

                while (string.IsNullOrWhiteSpace(adminName))
                {
                    Console.WriteLine("Enter Admin Name:");
                    adminName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(adminName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Admin Name cannot be empty. Please enter a valid Admin Name.");
                        Console.ResetColor();
                    }
                }

                while (string.IsNullOrWhiteSpace(adminPassword))
                {
                    Console.WriteLine("Enter Admin Password:");
                    adminPassword = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(adminPassword))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Admin Password cannot be empty. Please enter a valid Admin Password.");
                        Console.ResetColor();
                    }
                }

                isAuthenticated = ValidateAdminLogin(adminName, adminPassword);

                if (isAuthenticated)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login successful!");
                    Console.ResetColor();
                    dashBoard.DisplayMenu();
                    break;
                }
                else
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials. Login failed. You have " + (maxAttempts - attempts) + " attempts left.");
                    Console.ResetColor();
                }
            } while (attempts < maxAttempts);

            if (!isAuthenticated)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Maximum attempts reached. Please contact the administrator.");
                Console.ResetColor();
            }

            CloseConnection();
        }
        public void ChangePassword()
        {
            Console.WriteLine("Enter admin name:");
            string adminName = Console.ReadLine();

            Console.WriteLine("Enter current password:");
            string currentPassword = Console.ReadLine();

            Console.WriteLine("Enter new password:");
            string newPassword = Console.ReadLine();

            var admin = dbContext.Admins.FirstOrDefault(a => a.Name == adminName);

            if (admin != null)
            {
                if (admin.Password == currentPassword)
                {
                    admin.Password = newPassword;
                    dbContext.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Password changed successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid current password. Password change failed.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Admin not found. Password change failed.");
                Console.ResetColor();
            }
        }
        public Admin GetAdminByPasswordAndName(string password, string name)
        {
            return dbContext.Admins.FirstOrDefault(a => a.Password == password && a.Name == name);
        }
        private bool ValidateAdminLogin(string name, string password)
        {
            var admin = GetAdminByPasswordAndName(password, name);
            return admin != null;
        }
        private void CloseConnection()
        {
            dbContext.Dispose();
        }
    }
}

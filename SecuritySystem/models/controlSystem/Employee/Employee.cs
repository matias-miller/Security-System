/*
 * This class handles employee interactions from logging in, creating new accounts, permissioning, etc.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace controlSystem.Employee{

    public class Employee {

        public Employee() {
        }

        public int salary = 0;

        public string firstName = "test";

        public string lastName = "";

        public string address = "";

        public string userName = "";

        public string password = "";

        public bool isOnCall = false;

        public bool isSupervisor = true;

        public int employeeID = 0;

        /// <summary>
        /// Logs the user in
        /// @param userName 
        /// @param password 
        /// @return
        /// </summary>
        public bool login(string userName, string password)
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);

            // Search for matching employee
            foreach (Employee employee in employees)
            {
                if (employee.userName == userName && employee.password == password)
                {
                    // Set isSupervisor based on employee role
                    isSupervisor = employee.isSupervisor;
                    return true;
                }
            }

            return false; // No matching employee found
        }

        public bool logout() {
            // TODO implement here
            return true;
        }

        public bool checkIfAdmin() {
            // Setting isSupervisor will fix this
            return isSupervisor;
        
        }

        public int getSalary() {
            // TODO implement here
            return this.salary;
        }

        public string getPassword() {
            return this.password;
        }

        public bool addUser(string first, string last, string email, string password)
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);

            // Check for existing user
            foreach (Employee employee in employees)
            {
                if (employee.userName == email)
                {
                    // User already exists
                    return false;
                }
            }

            // Create a new employee
            Employee newEmployee = new Employee
            {
                firstName = first,
                lastName = last,
                userName = email,
                password = password,
                isSupervisor = false,
                employeeID = employees.Count + 1,
                salary = 0,
                isOnCall = false
            };

            // Add new employee to the list
            employees.Add(newEmployee);

            // Serialize the updated list back to JSON
            string updatedJson = JsonConvert.SerializeObject(employees, Formatting.Indented);

            // Write the updated JSON back to the file
            File.WriteAllText(fullPath, updatedJson);

            return true;
        }

        public string findOnCall() {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);

            // Search for the user by userName
            foreach (Employee employee in employees)
            {
                if (employee.isOnCall == true) { return employee.firstName; }
            }

            // No one is on call
            return "test";
        }

        public string findSupervisor()
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);

            // Search for the user by userName
            foreach (Employee employee in employees)
            {
                if (employee.isSupervisor == true) { return employee.firstName; }
            }

            // No one is a supervisor
            return "";
        }

        public bool promoteUser()
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);

            // Search for the user by userName
            foreach (Employee employee in employees)
            {
                if (employee.userName == userName)
                {
                    // Promote the user to supervisor
                    employee.isSupervisor = true;

                    // Serialize the updated list back to JSON
                    string updatedJson = JsonConvert.SerializeObject(employees, Formatting.Indented);

                    // Write the updated JSON back to the file
                    File.WriteAllText(fullPath, updatedJson);

                    return true;
                }
            }

            // User not found
            return false;
        }
        
    }
}
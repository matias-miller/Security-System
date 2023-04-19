using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace controlSystem.Employee{
    /// <summary>
    /// Employee is a standard employee that can either be a supervisor or a  general operator.
    /// </summary>
    public class Employee {

        /// <summary>
        /// Employee is a standard employee that can either be a supervisor or a  general operator.
        /// </summary>
        public Employee() {
        }

        /// <summary>
        /// How much the employee gets paid
        /// </summary>
        public int salary = 0;

        public string firstName = "";

        public string lastName = "";

        public string address = "";

        public string userName = "";

        public string password = "";

        /// <summary>
        /// This shows if the employee is on call or not
        /// </summary>
        public bool isOnCall = false;

        /// <summary>
        /// will be set in login
        /// </summary>
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

        /// <summary>
        /// @return
        /// </summary>
        public bool logout() {
            // TODO implement here
            return true;
        }

        /// <summary>
        /// the
        /// @return
        /// </summary>
        public bool requestToManControlCenter() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public string getName() {
            // TODO implement here
            return "";
        }

        /// <summary>
        /// @return
        /// </summary>
        public string getAddress() {
            // TODO implement here
            return "";
        }

        public bool checkIfAdmin() {
            // Setting isSupervisor will fix this
            return isSupervisor;
        
        }

        /// <summary>
        /// @return
        /// </summary>
        public string getUserName() {
            // TODO implement here
            return "";
        }

        /// <summary>
        /// @return
        /// </summary>
        public int getSalary() {
            // TODO implement here
            return this.salary;
        }
        public string getPassword() {
            return this.password;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool checkIfOnCall() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public int getEmployeeID() {
            // TODO implement here
            return 0;
        }

        /// <summary>
        /// This will call all the getter methods
        /// @return
        /// </summary>
        public bool getEmployeeData() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool becomeOnCall() {
            // TODO implement here
            return false;
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


    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EmployeeNamespace = controlSystem.Employee;

namespace controlSystem
{
    public class ControlCenter
    {

        public ControlCenter()
        {
        }
        //These need default values to work
        public bool alarmReported = false;

        public Employee.Employee employeeOnCall = new Employee.Employee();

        public bool isManned = false;

        public Employee.Employee mannedBy = new Employee.Employee();

        public Employee.Employee employee = new Employee.Employee();
        
        /// <summary>
        /// This holds all employee ids for validation
        /// </summary>
        public Employee.Employee[] employeeIDArray = new Employee.Employee[0];

        public buildingSystem.BuildingControlSystem buldingControlCenter = new buildingSystem.BuildingControlSystem();

        public PhoneSystem phoneSystem = new PhoneSystem();

        public int test = 0;
        /// <summary>
        /// This will load an employee object then request that the employee function becomeOnCall is used
        /// @param employeeID 
        /// @return
        /// </summary>
        public bool requestEmployeeToBeOnCall(string employeeID)
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// used to to trigger phoneSystem to make call
        /// @param employeeOnCallID 
        /// @return
        /// </summary>
        public bool requestToMakeCall(int employeeOnCallID)
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// when employee used requestToManControl center or when just the controlCenter wants to set the onCall employee
        /// @param employeeID 
        /// @return
        /// </summary>
        public bool manControlCenter(int employeeID)
        {
            // TODO implement here
            return false;
        }

        public string getOnCallOperator()
        {
            string onCallOperator = employee.findOnCall();
            return onCallOperator;
        }

        public string getOnDutySupervisor()
        {
            string supervisor = employee.findSupervisor();
            return supervisor;
        }

        /// <summary>
        /// calls determinAlarmSeverity and getBuildingState
        /// @return
        /// </summary>
        public bool alarmReportedProcedure()
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will call requestToModifyBuildingState from the buildingCenterObject
        /// @return
        /// </summary>
        public bool useBuildingControlSystem()
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// used in alarmReportedProcedure, I assume this will just be a yes no toggle that will check say the employee manually checked the alarm
        /// @return
        /// </summary>
        public bool confirmAlarmManually()
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will query sendBuildingStateToControlCenter to determine what state the building is in, will be called in alarmReportedProcedure
        /// @return
        /// </summary>
        public bool checkIfBuildingControlCenterConfirmedAlarm()
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool buildingStateListener()
        {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will call the buildingControlCenter function displayBuilding
        /// @return
        /// </summary>
        public bool requestThenDisplayBuildingView()
        {
            // TODO implement here
            return false;
        }

        public bool validateEmployeeLogin(string email, string password)
        {
            var success = employee.login(email, password);
            return success;
        }
        public bool attemptEmployeeLogout()
        {
            var success = employee.logout();
            return success;
        }

        public bool attemptToPromoteUser(string user)
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<EmployeeNamespace.Employee> employees = JsonConvert.DeserializeObject<List<EmployeeNamespace.Employee>>(jsonContent);

            // Find the employee with the specified username
            EmployeeNamespace.Employee employeeToPromote = employees.FirstOrDefault(employee => employee.userName == user);

            // If the employee is not found, return false
            if (employeeToPromote == null)
            {
                return false;
            }

            // Promote the employee
            employeeToPromote.isSupervisor = true;

            // Serialize the updated list of employees to JSON
            string updatedJson = JsonConvert.SerializeObject(employees, Formatting.Indented);
            // Write the updated JSON content back to the file
            File.WriteAllText(fullPath, updatedJson);

            return true;
        }

        public string[] returnAllNonAdmins()
        {
            // Read JSON file
            string relativePath = "login.json";
            // Combine the relative path with the current directory to get the full path
            string fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
            // Read the contents of the JSON file
            string jsonContent = File.ReadAllText(fullPath);
            // Deserialize JSON to list of Employees
            List<EmployeeNamespace.Employee> employees = JsonConvert.DeserializeObject<List<EmployeeNamespace.Employee>>(jsonContent);

            // Create a list to store the non-admin users
            List<string> nonAdminUsers = new List<string>();

            // Iterate through the list of employees and add non-admin users to the list
            foreach (EmployeeNamespace.Employee employee in employees)
            {
                if (!employee.isSupervisor)
                {
                    nonAdminUsers.Add(employee.userName);
                }
            } 

            return nonAdminUsers.ToArray();
        }

        public bool attemptAddUser(string first, string last, string email, string password) {

            var success = employee.addUser(first, last, email, password);
            return success;

        }

        public bool attemptCheckIfAdmin() {
            var success = employee.checkIfAdmin();
            return success;
        }

        public string testGetEmployeePassword() {
            Debug.WriteLine(employee.getPassword());
           return employee.getPassword();
        }
    }
}
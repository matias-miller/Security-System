
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public bool isSupervisor = false;

        public int employeeID;

        /// <summary>
        /// Logs the user in
        /// @param userName 
        /// @param password 
        /// @return
        /// </summary>
        public bool login(string userName, string password) {
            // TODO implement here
            this.password = password; 
            return true;
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
        public bool getSalary() {
            // TODO implement here
            return false;
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
        public object getEmployeeData() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool becomeOnCall() {
            // TODO implement here
            return false;
        }

        public bool promoteUser() {
            return true;
        }
        public bool addUser(string first, string last, string email, string password) {
            //TBD how you do this
            return true;
        }

    }
}
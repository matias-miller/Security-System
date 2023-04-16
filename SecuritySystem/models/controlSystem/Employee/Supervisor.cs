
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controlSystem.Employee{
    public class Supervisor : Employee {

        public Supervisor() {
        }

        /// <summary>
        /// Holds a list of there subordanants
        /// </summary>
        public bool arrayOfSubordanantsIds = false;

        public void addSupervisorOrGeneralOperator() {
            // TODO implement here
        }

        /// <summary>
        /// @param firstName 
        /// @param lastName 
        /// @param address 
        /// @param userName 
        /// @param password 
        /// @param isSupervisor 
        /// @param supervisorID
        /// </summary>
        public void addOperatorsOrSupervisors(string firstName, string lastName, string address, string userName, string password, bool isSupervisor, int supervisorID) {
            // TODO implement here
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool returnSubordanantsIds() {
            // TODO implement here
            return false;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controlSystem{
    public class PhoneSystem {

        public PhoneSystem() {
        }

        /// <summary>
        /// The number of the phone system
        /// </summary>
        public string callingFromNumber = "";

        public bool listOfNumbers = false;

        /// <summary>
        /// @return
        /// </summary>
        public bool callPolice() {
            // TODO
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool callOnCallEmployee() {
            // TODO
            return true;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool callFireDepartment() {
            // TODO
            return true;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool callChemicalDepartment() {
            // Returns true automatically to display pop-up confirmation of call
            // displays notification on page upon trigger
            return true;
        }

    }
}
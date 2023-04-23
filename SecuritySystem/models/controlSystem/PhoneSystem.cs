using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controlSystem{
    public class PhoneSystem {

        public PhoneSystem() {
        }
        public string callingFromNumber = "";

        public bool listOfNumbers = false;

        public bool callPolice() {
            return false;
        }
        public bool callOnCallEmployee() {
            return true;
        }
        public bool callFireDepartment() {
            return true;
        }

        public bool callChemicalDepartment() {
            return true;
        }
    }
}
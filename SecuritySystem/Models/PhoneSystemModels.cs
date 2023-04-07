using System;
namespace SecuritySystem.Models
{
    public class PhoneSystem
    {
        public string CallingFromPhoneNumber { get; set; }
        public List<string> ListOfNumbers { get; set; }

        public void CallPolice() { }
        public void CallOnCallEmployee() { }
        public void CallFireDepartment() { }
        public void CallChemicalDepartment() { }
    }
}


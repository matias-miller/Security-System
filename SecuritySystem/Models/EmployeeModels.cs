using System;
namespace SecuritySystem.Models
{
    public class Employee
    {
        public decimal Salary { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsOnCall { get; set; }

        public void Login() { }
        public void Logout() { }
        public void BecomeOnCall() { }
        public void RequestToManControlCenter() { }
    }

    public class Supervisor : Employee
    {
        public List<GeneralOperator> ListOfOperators { get; set; }

        public void AddSupervisorOrGeneralOperator() { }
    }

    public class GeneralOperator : Employee
    {
        public string SupervisorName { get; set; }
    }

}


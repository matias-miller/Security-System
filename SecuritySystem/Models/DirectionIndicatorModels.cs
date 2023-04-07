using System;
namespace SecuritySystem.Models
{
    public class DirectionIndicators
    {
        public bool IsActive { get; set; }

        public void TurnOnIndicators() { }
        public void TurnOffIndicators() { }
        public void SendStateOnChange() { }
    }

}


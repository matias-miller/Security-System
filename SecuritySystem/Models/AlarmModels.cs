using System;
namespace SecuritySystem.Models
{

    public class Alarm
    {
        public bool IsActive { get; set; }
        public bool IsSmokeAlarm { get; set; }
        public bool IsGasAlarm { get; set; }

        public void ActivateAlarm() { }
        public void DeactivateAlarm() { }
        public void SendStateOnChange() { }
    }

}
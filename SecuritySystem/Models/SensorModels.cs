using System;
namespace SecuritySystem.Models
{
    public class Sensor
    {
        public bool IsActive { get; set; }
        public bool IsGasSensor { get; set; }
        public bool IsSmokeSensor { get; set; }

        public void ActivateSensor() { }
        public void DeactivateSensor() { }
        public void SendStateOnChange() { }
    }

    public class GasSensor : Sensor
    {
        public void DetectGas() { }
    }

    public class SmokeSensor : Sensor
    {
        public void DetectSmoke() { }
    }

}


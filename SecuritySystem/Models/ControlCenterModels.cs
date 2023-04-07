using System;
namespace SecuritySystem.Models
{
    public class ControlCenter
    {
        public bool IsManned { get; set; }
        public string EmployeeOnCall { get; set; }
        public string AlarmReported { get; set; }

        public void UseBuildingControlSystem() { }
        public void AlarmReportedProcedure() { }
        public void DetermineAlarmSeverity() { }
        public void ManControlCenter() { }
        public void RequestToMakeCall() { }
        public void RequestInformationFromCameraSystem() { }
        public void RequestEmployeeToBeOnCall() { }
        public void RequestBuildingState() { }
        public void SendNotificationToBuildingControlCenter() { }
    }

}


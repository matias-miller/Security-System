using System;
namespace SecuritySystem.Models
{
    public class BuildingControlSystem
    {
        public string BuildingState { get; set; }

        public void SendBuildingStateToControlCenter() { }
        public void ReportAlarmToControlCenter() { }
        public void SendBuildingViewToControlCenter() { }
        public void RequestToViewBuilding() { }
        public void UpdateBuildingState() { }
    }

    public class Building
    {
        public List<Room> RoomList { get; set; }

        public void ViewBuilding() { }
        public void RequestToViewRoom() { }
        public void SendBuildingStateOnChange() { }
    }

    public class Room
    {
        public string RoomName { get; set; }
        public string Location { get; set; }
        public bool IsElectricityActive { get; set; }
        public string Zone { get; set; }

        public void ViewRoom() { }
        public void TurnOffElectricalEquipment() { }
        public void RequestTurnOnOffDirectionIndicators() { }
        public void RequestLockUnlockDoor() { }
        public void SendRoomStateOnChange() { }
        public void RequestDoorLockUnlock() { }
        public void RequestTurnOnOffSprinklers() { }
        public void RequestTurnOnOffSensor() { }
        public void RequestTurnOnOffAlarm() { }
    }

}


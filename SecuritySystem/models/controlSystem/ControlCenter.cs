
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controlSystem{
    public class ControlCenter {

        public ControlCenter() {
        }

        private bool alarmReported;

        private Employee.Employee employeeOnCall;

        private bool isManned;

        private Employee.Employee mannedBy;

        /// <summary>
        /// This holds all employee ids for validation
        /// </summary>
        private object employeeIDArray;

        private buildingSystem.BuildingControlSystem buldingControlCenter;

        private PhoneSystem phoneSystem;

        /// <summary>
        /// This will load an employee object then request that the employee function becomeOnCall is used
        /// @param employeeID 
        /// @return
        /// </summary>
        public bool requestEmployeeToBeOnCall(string employeeID) {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// used to to trigger phoneSystem to make call
        /// @param employeeOnCallID 
        /// @return
        /// </summary>
        public bool requestToMakeCall(int employeeOnCallID) {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// when employee used requestToManControl center or when just the controlCenter wants to set the onCall employee
        /// @param employeeID 
        /// @return
        /// </summary>
        public bool manControlCenter(int employeeID) {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// calls determinAlarmSeverity and getBuildingState
        /// @return
        /// </summary>
        public bool alarmReportedProcedure() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will call requestToModifyBuildingState from the buildingCenterObject
        /// @return
        /// </summary>
        public bool useBuildingControlSystem() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// used in alarmReportedProcedure, I assume this will just be a yes no toggle that will check say the employee manually checked the alarm
        /// @return
        /// </summary>
        private bool confirmAlarmManually() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will query sendBuildingStateToControlCenter to determine what state the building is in, will be called in alarmReportedProcedure
        /// @return
        /// </summary>
        public bool checkIfBuildingControlCenterConfirmedAlarm() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool buildingStateListener() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will call the buildingControlCenter function displayBuilding
        /// @return
        /// </summary>
        public bool requestThenDisplayBuildingView() {
            // TODO implement here
            return false;
        }

    }
}
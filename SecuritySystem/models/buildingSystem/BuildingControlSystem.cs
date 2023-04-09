
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem{
    public class BuildingControlSystem {

        public BuildingControlSystem() {
        }

        /// <summary>
        /// holds a object, likely json holding the building state
        /// </summary>
        private object buildingState;

        /// <summary>
        /// Creates a building object to be used in below functions
        /// </summary>
        private Building building;

        /// <summary>
        /// Holds a camera system object of CameraSystem to be used in below functions
        /// </summary>
        private cameraSystem.CameraSystem cameraSystem;

        /// <summary>
        /// When the buildingControlCenter object in control center wants the buildingState it will be returned via this.
        /// NEEDS TO STAY THE SAME
        /// @return
        /// </summary>
        public object sendBuildingStateToControlCenter() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// @return
        /// </summary>
        public object reportAlarmToControlCenter() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// In the ControlCenter when the buildingControlCenter object wants to display the building it will display a up to date view of the building, using buildingState to populate the view. This will actually populate the screen
        /// NEEDS TO STAY THE SAME
        /// @return
        /// </summary>
        public object displayBuilding() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// Runs in background to update the buildingState variable for sendBuildingViewToControlCenter to display the building. Will likely be in the form of button
        /// will call Building.setndBuildingState.
        /// @return
        /// </summary>
        private bool updateBuildingState() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Used by buildingControl center to request that actions are performed on the room such as the room function requestTurnOnOffDirectionIndecators. It will sequentially call the building function requestToModifyRoomState.
        /// NEEDS TO STAY THE SAME
        /// @param requestType 
        /// @param location This should be an object that describes the room and what sensor needs to be changed
        /// @return
        /// </summary>
        public bool requestToModifyBuildingState(string requestType, string location) {
            // TODO implement here
            return false;
        }

    }
}
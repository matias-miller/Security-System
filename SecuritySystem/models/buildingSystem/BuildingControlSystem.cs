
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Collections.Specialized.BitVector32;

namespace buildingSystem{
    public class BuildingControlSystem {
        //Debug.WriteLine("in");
        public BuildingControlSystem() {
            //Start by creating a building object, this will funnel into the building object
            building = new Building();
            
        }

        /// <summary>
        /// holds a object, likely json holding the building state
        /// </summary>
        private object buildingState;


        /// <summary>
        /// Creates a building object to be used in below functions
        /// </summary>
        private static Building building;

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
        
  

        public bool setSpecificRoomState(int room,bool value) {
            // just a test function
            building.requestToModifyRoomState("requestTurnOnOffSensor", room, value);
            return true;
        }

        public bool getSpecificRoomState(int room)
        {
            //function used to get the specific state of a room
            return building.requestSpecificSensorState(room);
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
        /// This is the name of the function to be called in room
        /// @param roomNumber
        /// this is the room
        /// @return
        /// </summary>
        public bool requestToModifyBuildingState(string requestType, int roomNumber, bool action) {
            var data = building.requestToModifyRoomState(requestType, roomNumber, action);
            return data;
        }

    }
}
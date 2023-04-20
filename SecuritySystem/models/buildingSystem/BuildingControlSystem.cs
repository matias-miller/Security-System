
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;

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
        public bool buildingState = false;


        /// <summary>
        /// Creates a building object to be used in below functions
        /// </summary>
        [JsonProperty]
        public static Building building = new Building();

        /// <summary>
        /// Holds a camera system object of CameraSystem to be used in below functions
        /// </summary>
        public cameraSystem.CameraSystem cameraSystem = new cameraSystem.CameraSystem();

        /// <summary>
        /// When the buildingControlCenter object in control center wants the buildingState it will be returned via this.
        /// NEEDS TO STAY THE SAME
        /// @return
        /// </summary>
        public bool sendBuildingStateToControlCenter() {
            // TODO implement here
            return false;
        }

        public object RequestToAddSensorToRoom(int room) {
            
             building.roomList[room].sensor.addSensorToRoom();
            
            return getRoomsWithSensors() ;
        }

        public object RequestToRemoveSensorFromRoom(int room)
        {

            building.roomList[room].sensor.RemoveSensorFromRoom();

            return getRoomsWithSensors();
        }



        public object getRoomsWithSensors() {
            List<int> roomsWithSensors = new List<int>();
            // get all sensors that are active
            for (int i = 0; i <= 40; i++)
            {
                
                if (building.roomList[i].sensor.isInRoom)
                {
                    Debug.WriteLine(i);
                    if (i != 1 || i != 38) {
                       
                        roomsWithSensors.Add(i + 1);
                    }
                 
                }

            }

            return roomsWithSensors;

        }

        public bool setSpecificRoomState(int room,bool value) {
            // just a test function
            Debug.WriteLine("is in " + building.roomList[room].sensor.isInRoom);
            if (building.roomList[room].sensor.isInRoom)
            {
                building.requestToModifyRoomState("requestTurnOnOffSensor", room, value);
                return true;
            }
            else {
                return false;
            }
    
        }
     

        public bool getSpecificRoomState(int room)
        {
            //function used to get the specific state of a room
            Debug.WriteLine("Room2: " + room);
            return building.requestSpecificSensorState(room);
        }

        public bool getSpecificSensorState(int room)
        {
            //function used to get the specific state of a room
            return building.requestSpecificSensorState(room);
        }

        public bool attemptToAddPersonToRoom(int room) {
                return building.roomList[room].addPersonToRoom();
        }

        public bool attemptToRemovePersonToRoom(int room)
        {
            return building.roomList[room].removePersonToRoom();
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool reportAlarmToControlCenter() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// In the ControlCenter when the buildingControlCenter object wants to display the building it will display a up to date view of the building, using buildingState to populate the view. This will actually populate the screen
        /// NEEDS TO STAY THE SAME
        /// @return
        /// </summary>
        public bool displayBuilding() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Runs in background to update the buildingState variable for sendBuildingViewToControlCenter to display the building. Will likely be in the form of button
        /// will call Building.setndBuildingState.
        /// @return
        /// </summary>
        public bool updateBuildingState() {
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

        public int getNumberOfActiveSensors() {
            // used to get the number of active alarms to confirm alarm
            var number = building.getNumberOfActiveSensors();
            return number;
        }

        public object requestToActivateSprinklersAutomated() {
            var sprinklers = building.activateSprinklersAutomated();
            return sprinklers;
        }
        public object requestToActivateAlarmsAutomated()
        {
            var alarms = building.activateAlarmsAutomated();
            return alarms;

        }
        public object requestToActivateDirectionsAutomated()
        {
            var directions = building.activateDirectionsAutomated();
            return directions;

        }
        public object requestToActivateDoorsAutomated()
        {
            var doors = building.activateDoorsAutomated();
            return doors;

        }
        public object requestToMakeCallsAutomated()
        {
            var calls = building.makeCallsAutomated();
            return calls;

        }

    }
}
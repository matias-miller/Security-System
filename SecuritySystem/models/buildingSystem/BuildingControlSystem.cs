//  The building control center is the conduate to Building object fielding all requests
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
        public BuildingControlSystem() {
            //Start by creating a building object, this will funnel into the building object
            building = new Building();
            
        }

        /// <summary>
        /// Creates a building object to be used in below functions
        /// </summary>
        [JsonProperty]
        public static Building building = new Building();

        public object RequestToAddSensorToRoom(int room) {
            // This is how the building view gets a sensor added to the room
             building.roomList[room].sensor.addSensorToRoom();
            return getRoomsWithSensors() ;
        }

        public object RequestToRemoveSensorFromRoom(int room)
        {
            // How the the sensors are removed from room
            building.roomList[room].sensor.RemoveSensorFromRoom();
            return getRoomsWithSensors();
        }
        public object getRoomsWithSensors() {
            // References the rooms that currently have sensors, for displaying on the gui.
            List<int> roomsWithSensors = new List<int>();
            // get all sensors that are active
            for (int i = 0; i <= 40; i++)
            {
                if (building.roomList[i].sensor.isInRoom)
                {
                    // these two rooms dont have sensors
                    if (i != 1 || i != 38) {
                        roomsWithSensors.Add(i + 1);
                    }
                 
                }
            }
            return roomsWithSensors;
        }

        public string getRoomStateJson(int room)
        {
            //function used to get all of the states in a room from building classsss
            return building.requestRoomStateJson(room);
        }

        public bool getSpecificRoomState(int room)
        {
            //function used to get the specific state of a room
            return building.requestSpecificSensorState(room);
        }

        public bool getSpecificSensorState(int room)
        {
            //function used to get the specific state of a room
            return building.requestSpecificSensorState(room);
        }

        public bool attemptToAddPersonToRoom(int room) {
            // How the building adds people to the room 
            return building.roomList[room].addPersonToRoom();
        }

        public bool attemptToRemovePersonToRoom(int room)
        {
            // how to building removes people from the room
            return building.roomList[room].removePersonToRoom();
        }


        public bool requestToModifyBuildingState(string requestType, int roomNumber, bool action) {
            // a helper function to get the building to request something about the room changes
            var data = building.requestToModifyRoomState(requestType, roomNumber, action);
            return data;
        }

        public int getNumberOfActiveSensors() {
            // used to get the number of active alarms to confirm alarm
            var number = building.getNumberOfActiveSensors();
            return number;
        }

        public object requestToActivateSprinklersAutomated() {
            // helper to request that sprinklers are activated
            var sprinklers = building.activateSprinklersAutomated();
            return sprinklers;
        }
        public object requestToActivateAlarmsAutomated()
        {
            // how to building automatically activates alarms
            var alarms = building.activateAlarmsAutomated();
            return alarms;

        }
        public object requestToActivateDirectionsAutomated()
        {
            // How the building requests that directions be automatically activated
            var directions = building.activateDirectionsAutomated();
            return directions;

        }
        public object requestToActivateDoorsAutomated()
        {
            // how the building control requests doors be automatically closed
            var doors = building.activateDoorsAutomated();
            return doors;

        }
        public object requestToMakeCallsAutomated(bool Gas)
        {
            // how calls are automatically requested to be made
            var calls = building.makeCallsAutomated(Gas);
            return calls;

        }

    }
}
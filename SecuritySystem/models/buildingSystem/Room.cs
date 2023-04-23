// Room is basically the container for all of the elements in the room
using buildingSystem.roomComponents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace buildingSystem{
    public class Room {

        public Room(int numberOfDoors, string zone, int roomName) {
            // upon creating the room we need to initialize things like sprinklers, doors, and direction indicators
            this.zone = zone;
            this.roomName = roomName;
            this.numberOfDoors = numberOfDoors;

            // set up the number of doors each room has
            this.doorArray = new roomComponents.Door[numberOfDoors];
            for (int i = 0; i < numberOfDoors; i++) { 
                this.doorArray[i] = new roomComponents.Door();
            }

            // default the electricity to true by default
            this.isElectricityActive = true;

            this.directionIndecator = new roomComponents.DirectionIndicators();
            this.sprinkler = new roomComponents.Sprinklers();
            this.sensor = new roomComponents.Sensor();
            this.alarm = new roomComponents.Alarm();
        }

        public bool isElectricityActive = false;

        public bool peoplePresent = false;

        // zone is where the room is located
        public string zone = "";

        public int roomName = 0;
        public int numberOfDoors = 0;

        public roomComponents.DirectionIndicators directionIndecator = new DirectionIndicators();

        public roomComponents.Door[] doorArray = new roomComponents.Door[0];

        public roomComponents.Sprinklers sprinkler = new roomComponents.Sprinklers();

        public roomComponents.Sensor sensor = new roomComponents.Sensor();

        public roomComponents.Alarm alarm = new roomComponents.Alarm();


        public bool addPersonToRoom() {
            // This adds people to the room
            this.peoplePresent = true;
            return this.peoplePresent;
        }
        public bool removePersonToRoom()
        {
            // This removes people to the room
            this.peoplePresent = false;
            return this.peoplePresent;
        }

        // Method to return the room state as a JSON object
        public string GetRoomStateAsJson()
        {
            // Create an anonymous object to represent the room state
            var roomState = new
            {
                zone = this.zone,
                roomName = this.roomName,
                numberOfDoors = this.numberOfDoors,
                isElectricityActive = this.isElectricityActive,
                peoplePresent = this.peoplePresent,
                doorArray = this.doorArray,
                sprinkler = this.sprinkler,
                sensor = this.sensor,
                alarm = this.alarm,
            };

            // Serialize the anonymous object to JSON
            var json = JsonConvert.SerializeObject(roomState);
            return json;
        }

        public bool requestTurnOnOffSensor(bool action)
        // This handles how the sprinklers are turned on and off
        {
            // Requests the sensors are turned on or off
            var data = false;
            if (action == true)
            {
            
                data = this.sensor.activateSensor();
                this.isElectricityActive = false;
            }
            else
            {
                data = this.sensor.deactivateSensor();
                this.isElectricityActive = true;
            }
            return data;
        }

        public bool performAction(string actionType, bool action) {
            var data = false;
            // this is used to turn on or off sensor
            if (actionType == "requestTurnOnOffSensor")
            {
                data = requestTurnOnOffSensor(action);
            }
            return data;
        }

        public bool requestSpecificSensorState()
        {
            // This function gets the current state of a specific sensor
            return sensor.sendState();
        }

    }
}

using buildingSystem.roomComponents;
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
            this.zone = zone;
            this.roomName = roomName;
            this.numberOfDoors = numberOfDoors;

            // set up the number of doors each room has
            this.doorArray = new roomComponents.Door[numberOfDoors];
            for (int i = 0; i < numberOfDoors; i++) { 
                this.doorArray[i] = new roomComponents.Door();
            }

            this.isElectricityActive = true;

            this.directionIndecator = new roomComponents.DirectionIndicators();
            this.sprinkler = new roomComponents.Sprinklers();
            this.sensor = new roomComponents.Sensor();
            this.alarm = new roomComponents.Alarm();
            // camera is kinda pointless
            this.camera = new cameraSystem.Camera();
        }

        public bool isElectricityActive = false;

        /// <summary>
        /// This is the zone the room is located in
        /// </summary>
        public string zone = "";

        public int roomName = 0;
        public int numberOfDoors = 0;

        public roomComponents.DirectionIndicators directionIndecator = new DirectionIndicators();

        public roomComponents.Door[] doorArray = new roomComponents.Door[0];

        public roomComponents.Sprinklers sprinkler = new roomComponents.Sprinklers();

        public roomComponents.Sensor sensor = new roomComponents.Sensor();

        public roomComponents.Alarm alarm = new roomComponents.Alarm();

        public cameraSystem.Camera camera = new cameraSystem.Camera();

        //public static int test;

        //public void setTest() {
        //    test = 4;
        //}
        //public void getTest() {
        //    Debug.WriteLine(test);
        //}

        /// <summary>
        /// This holds the room attributes in a object to be sent to building
        /// </summary>
        public bool roomState = false;

        public void requestTurnOnOffAlarm() {
            // TODO implement here
        }

        /// <summary>
        /// changes the variable isElectricityActive
        /// @return
        /// </summary>
        public bool turnOffElectricalEquipment() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool requestTurnOnOffDirectionIndecators() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool requestLockUnlockDoor() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This returns the roomStateObject to calling function
        /// @return
        /// </summary>
        public bool sendRoomStateOnChange() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool requestDoorLockUnlock() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool requestTurnOnOffSprinklers() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool requestTurnOnOffSensor(bool action)
        {
            // Requests the sensors are turned on or off
            var data = false;
            if (action == true)
            {
                data = this.sensor.activateSensor();
            }
            else
            {
                data = this.sensor.deactivateSensor();
            }
            return data;
        }

        /// <summary>
        /// Sets all of the information about the room like name, and the arrays contained by the room, such as doorArray based on there current states. Then packs that all into the variable roomState
        /// @return
        /// </summary>
        public bool updateRoomState() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// this will call any of the request or turn on off functions in Room, called by building to change things like sensors or DirectionIndicators
        /// @param actionType 
        /// @return
        /// </summary>
        public bool performAction(string actionType, bool action) {
            var data = false;
            // action type is the name of the method to be called
            if (actionType == "requestTurnOnOffAlarm") 
            {
            
            } 
            else if(actionType == "turnOffElectricalEquipment")
            {

            }
            else if (actionType == "requestTurnOnOffDirectionIndecators")
            {

            }
            else if (actionType == "requestLockUnlockDoor")
            {

            }
            else if (actionType == "sendRoomStateOnChange")
            {

            }
            else if (actionType == "requestDoorLockUnlock")
            {

            }
            else if (actionType == "requestTurnOnOffSprinklers")
            {

            }
            else if (actionType == "requestTurnOnOffSensor")
            {
                data = requestTurnOnOffSensor(action);
            }
            else if (actionType == "updateRoomState")
            {

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
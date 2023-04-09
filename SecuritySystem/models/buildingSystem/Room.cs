
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem{
    public class Room {

        public Room() {
        }

        private bool isElectricityActive;

        /// <summary>
        /// This is the zone the room is located in
        /// </summary>
        private string zone;

        private string roomName;

        private roomComponents.DirectionIndicators directionIndecatorArray;

        private roomComponents.Door doorArray;

        private roomComponents.Sprinklers sprinklerArray;

        private roomComponents.Sensor sensorArray;

        private roomComponents.Alarm alarmArray;

        private cameraSystem.Camera cameraArray;

        /// <summary>
        /// This holds the room attributes in a object to be sent to building
        /// </summary>
        private object roomState;

        private void requestTurnOnOffAlarm() {
            // TODO implement here
        }

        /// <summary>
        /// changes the variable isElectricityActive
        /// @return
        /// </summary>
        private bool turnOffElectricalEquipment() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        private bool requestTurnOnOffDirectionIndecators() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        private bool requestLockUnlockDoor() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This returns the roomStateObject to calling function
        /// @return
        /// </summary>
        private object sendRoomStateOnChange() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// @return
        /// </summary>
        private bool requestDoorLockUnlock() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        private bool requestTurnOnOffSprinklers() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        private bool requestTurnOnOffSensor() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Sets all of the information about the room like name, and the arrays contained by the room, such as doorArray based on there current states. Then packs that all into the variable roomState
        /// @return
        /// </summary>
        private bool updateRoomState() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// this will call any of the request or turn on off functions in Room, called by building to change things like sensors or DirectionIndicators
        /// @param actionType 
        /// @return
        /// </summary>
        public bool performAction(string actionType) {
            // TODO implement here
            return false;
        }

    }
}
/* This class handles turning on and off the alarm.
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Sensor {

        public Sensor() {
        }

        public bool isActive;

        public bool isInRoom = false;
        public string alarmType = "Fire";
        public bool addSensorToRoom() {
            this.isInRoom = true;
            Debug.WriteLine(this.isInRoom);
            return this.isInRoom;
        }

        public bool RemoveSensorFromRoom()
        {
            // Removes sensor from a room
            this.isInRoom = false;
            return this.isInRoom;
        }

        public bool activateSensor() {
            // Actives sensor in a room
            if (this.isInRoom != false) {
                this.isActive = true;
                return this.isActive;
            } else { return false; }
        }

        public bool deactivateSensor() {
            // Deactives alarm sensor
            this.isActive = false;
            return this.isActive;
        }

        public bool sendState() {
            // Sends state of alarm sensor
            return this.isActive;
        }
    }
}
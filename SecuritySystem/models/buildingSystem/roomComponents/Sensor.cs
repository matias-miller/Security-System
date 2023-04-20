
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

        public bool isGasSensor = false;

        public bool isSmokeSensor = true;

        public int sensorID = 0;


        public bool isInRoom = false;


        public bool addSensorToRoom() {
            this.isInRoom = true;
            Debug.WriteLine(this.isInRoom);
            return this.isInRoom;
        }

        public bool RemoveSensorFromRoom()
        {
            this.isInRoom = false;
            return this.isInRoom;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool activateSensor() {
            // TODO implement here
                this.isActive = true;
            
           
            return this.isActive;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool deactivateSensor() {
            // TODO implement here
            this.isActive = false;
            return this.isActive;
        }

        /// <summary>
        /// returns the Sensor variables
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return this.isActive;
        }

    }
}
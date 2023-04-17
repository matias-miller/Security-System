
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Sensor {

        public Sensor() {
        }

        public bool isActive;

        public bool isGasSensor = false;

        public bool isSmokeSensor = false;

        public int sensorID = 0;

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
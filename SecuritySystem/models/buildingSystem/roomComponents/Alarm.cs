
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    /// <summary>
    /// Test documentation
    /// </summary>
    public class Alarm {

        /// <summary>
        /// Test documentation
        /// </summary>
        public Alarm() {
        }

        public bool isSmokeAlarm = false;

        public bool isGasAlarm = false;

        public int alarmID = 0;

        public bool isActive = false;

        /// <summary>
        /// testsets
        /// @return
        /// </summary>
        public bool activateAlarm() {
            // TODO implement here
            this.isActive = true;
            return this.isActive;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool deactivateAlarm() {
            // TODO implement here
            this.isActive = false;
            return this.isActive;
        }

        /// <summary>
        /// Returns alarm variables
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return this.isActive;
        }

    }
}
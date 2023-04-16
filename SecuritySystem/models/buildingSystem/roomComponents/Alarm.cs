
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
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool deactivateAlarm() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Returns alarm variables
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return false;
        }

    }
}
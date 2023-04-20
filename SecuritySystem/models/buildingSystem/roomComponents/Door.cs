
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Door {

        public Door() {
        }

        public bool isLocked = false;

        public int doorID = 0;

        /// <summary>
        /// @return
        /// </summary>
        public bool lockDoor() {
            // TODO implement here
            this.isLocked = true;
            return this.isLocked;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool unlockDoor() {
            // TODO implement here
            this.isLocked = false;
            return this.isLocked;
        }

        /// <summary>
        /// returns the door attributes
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return this.isLocked;
        }

    }
}
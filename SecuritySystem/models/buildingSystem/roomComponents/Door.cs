
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Door {

        public Door() {
        }

        public bool isLocked;

        public int doorID;

        /// <summary>
        /// @return
        /// </summary>
        public bool lockDoor() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool unlockDoor() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// returns the door attributes
        /// @return
        /// </summary>
        public object sendState() {
            // TODO implement here
            return null;
        }

    }
}
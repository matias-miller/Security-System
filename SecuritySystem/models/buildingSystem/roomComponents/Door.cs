/* This class handles turning locking and unlocking the doors
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Door {

        public Door() {
        }

        public bool isLocked = false;

        public bool lockDoor() {
            // This function locks the door
            this.isLocked = true;
            return this.isLocked;
        }

        public bool unlockDoor() {
            // This function unlocks the door
            this.isLocked = false;
            return this.isLocked;
        }
    }
}
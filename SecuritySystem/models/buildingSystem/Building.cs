
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem{
    public class Building {

        public Building() {
        }

        /// <summary>
        /// holds a array of the Room objects
        /// </summary>
        private Room roomList;

        /// <summary>
        /// Sends the building control system the building state when its been changed. Sending the buildingState determined in roomlist to the BuildingControlSystem function updateBuildingStateOnChange, that will update the buildingState variable. Likely will be in the form of a button
        /// @return
        /// </summary>
        public bool sendBuildingState() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Called after requestToModifyBuildingState has been called. This will call performAction in Room
        /// @param requestType 
        /// @param room 
        /// @return
        /// </summary>
        public bool requestToModifyRoomState(string requestType, Room room) {
            // TODO implement here
            return false;
        }

    }
}
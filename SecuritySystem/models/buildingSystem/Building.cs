
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace buildingSystem{
    public class Building {

        public Building() {
            // set up the array of the different rooms
            this.roomList = new Room[41];

            // Set up the individual rooms
            createRooms();
    
        }

        /// <summary>
        /// holds a array of the Room objects
        /// </summary>
        public Room[] roomList;

        /// <summary>
        /// Sends the building control system the building state when its been changed. Sending the buildingState determined in roomlist to the BuildingControlSystem function updateBuildingStateOnChange, that will update the buildingState variable. Likely will be in the form of a button
        /// @return
        /// </summary>
        public bool sendBuildingState() {
            // TODO implement here
            return false;
        }

        public void createRooms() {
            this.roomList[0] = new Room(1, "one", 1);
            this.roomList[1] = new Room(1, "one", 2);
            this.roomList[2] = new Room(1, "one", 3);
            this.roomList[3] = new Room(1, "one", 4);
            this.roomList[4] = new Room(1, "one", 5);
            this.roomList[5] = new Room(1, "two", 6);
            this.roomList[6] = new Room(1, "two", 7);
            this.roomList[7] = new Room(1, "two", 8);
            this.roomList[8] = new Room(2, "two", 9);
            this.roomList[9] = new Room(1, "two", 10);
            this.roomList[10] = new Room(1, "three", 11);
            this.roomList[11] = new Room(1, "three", 12);
            this.roomList[12] = new Room(1, "three", 13);
            this.roomList[13] = new Room(2, "three", 14);
            this.roomList[14] = new Room(2, "three", 15);
            this.roomList[15] = new Room(2, "four", 16);
            this.roomList[16] = new Room(1, "four", 17);
            this.roomList[17] = new Room(1, "four", 18);
            this.roomList[18] = new Room(1, "four", 19);
            this.roomList[19] = new Room(1, "four", 20);
            this.roomList[20] = new Room(2, "five", 21);
            this.roomList[21] = new Room(2, "five", 22);
            this.roomList[22] = new Room(1, "five", 23);
            this.roomList[23] = new Room(1, "five", 24);
            this.roomList[24] = new Room(1, "five", 25);
            this.roomList[25] = new Room(1, "six", 26);
            this.roomList[26] = new Room(1, "six", 27);
            this.roomList[27] = new Room(1, "six", 28);
            this.roomList[28] = new Room(1, "six", 29);
            this.roomList[29] = new Room(1, "six", 30);
            this.roomList[30] = new Room(1, "seven", 31);
            this.roomList[31] = new Room(1, "seven", 32);
            this.roomList[32] = new Room(1, "seven", 33);
            this.roomList[33] = new Room(1, "seven", 34);
            this.roomList[34] = new Room(1, "seven", 35);
            this.roomList[35] = new Room(1, "eight", 36);
            this.roomList[36] = new Room(1, "eight", 37);
            this.roomList[37] = new Room(1, "eight", 38);
            this.roomList[38] = new Room(1, "eight", 39);
            this.roomList[39] = new Room(1, "eight", 40);
            this.roomList[40] = new Room(1, "nine", 41);
        }

        /// <summary>
        /// Called after requestToModifyBuildingState has been called. This will call performAction in Room
        /// @param requestType 
        /// @param room 
        /// @return
        /// </summary>
        public bool requestToModifyRoomState(string requestType, int roomNumber, bool action) {
            // TODO implement here
            var data = this.roomList[roomNumber].performAction(requestType, action);
            return data;
        }

        public bool requestSpecificSensorState(int roomName) {
            // test function, can be removed
  
            return this.roomList[roomName].requestSpecificSensorState();
        }

    }
}
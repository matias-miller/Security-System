// Building controls creating the rooms, and requesting things about the room change
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

        public void createRooms() {
            // This initializes the rooms
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

        public bool requestToModifyRoomState(string requestType, int roomNumber, bool action) {
            // This function is how the building requests to modify the state of the sensor
            var data = this.roomList[roomNumber].performAction(requestType, action);
            return data;
        }

        public bool requestSpecificSensorState(int roomName) {
            // Gets the state of a specific sensor
            return this.roomList[roomName].requestSpecificSensorState();
        }
        
        public int getNumberOfActiveSensors() {
            // This function is used to get the number of currently active sensors, to determine if the alarm reported procedure is needed
            var count = 0;
            for (int i = 0; i <= 40; i++) {
                if (this.roomList[i].sensor.sendState()) {
                    var room = i + 1;
                    count = count + 1;
                }
            }
            return count;
        }

        public string requestRoomStateJson(int roomNumber)
        {
            // Returns Json room state from room class
            return this.roomList[roomNumber].GetRoomStateAsJson();
        }

        public object activateSprinklersAutomated()
        {
            // This loops through all of the ssensors checking if they are active, if they are the sprinkler in the room should turn on
            int[] sprinklers =  { };
            for (int i = 0; i <= 40; i++)
            {
                var room = i + 1;
                if (this.roomList[i].sensor.sendState())
                { // Sensor is on
                    // Push the on sprinkler to an array
                    Array.Resize(ref sprinklers, sprinklers.Length + 1);
                    sprinklers[sprinklers.Length - 1] = i + 1;
                    this.roomList[i].sprinkler.activateSprinkler();
                }
                else {
                    // Sensor is off
                    this.roomList[i].sprinkler.turnOffSprinkler();
                }
            }
            return sprinklers;
        }
        public object activateAlarmsAutomated()
        {
            // This loops through all of the ssensors checking if they are active, if they are the alarm in the room should turn on
            int[] alarms = { };
            for (int i = 0; i <= 40; i++)
            {
                if (this.roomList[i].sensor.sendState())
                { // Sensor is on
                    // Push the on alarm to an array
                    Array.Resize(ref alarms, alarms.Length + 1);
                    alarms[alarms.Length - 1] = i + 1;
                    this.roomList[i].alarm.activateAlarm();
                }
                else
                {
                    // Sensor is off
                    this.roomList[i].alarm.deactivateAlarm();
                }
            }
            return alarms;
        }
        public object activateDirectionsAutomated()
        {
            // This loops through all of the sensor checking if they are active, if they are the direction in the room should turn on
            int[] direction = { };
            for (int i = 0; i <= 40; i++)
            {
                if (this.roomList[i].sensor.sendState())
                { // Sensor is on
                    // Push the on direction indicator to an array
                    Array.Resize(ref direction, direction.Length + 1);
                    direction[direction.Length - 1] = i + 1;
                    this.roomList[i].directionIndecator.turnOnIndicators();
                }
                else
                {
                    // Sensor is off
                    this.roomList[i].directionIndecator.turnOffIndicators();
                }
            }
            return direction;

        }
        public object activateDoorsAutomated()
        {
            // This loops through all of the sensors checking if they are active, if they are the door in the room should turn on
            int[] doors = { };
            for (int i = 0; i <= 40; i++)
            {
                if (this.roomList[i].sensor.sendState())
                { // Sensor is on
                    // Push the on door to an array if people are not in the room
                    if (!this.roomList[i].peoplePresent) {
                        Array.Resize(ref doors, doors.Length + 1);
                        doors[doors.Length - 1] = i + 1;
                        // lock all doors in room
                        for (int j = 0; j < this.roomList[i].numberOfDoors; j++)
                        {
                            this.roomList[i].doorArray[j].lockDoor();
                        }
                    }
                }
                else
                {
                    // Sensor is off
                    //unlock all doors
                    for (int j = 0; j < this.roomList[i].numberOfDoors; j++)
                    {
                        this.roomList[i].doorArray[j].unlockDoor();
                    }
                }
            }
            return doors;
        }

        public object makeCallsAutomated(bool Gas)
        {
            // If the rooms sensor is active then we should push who to call to an array
            // If the type is gas it will be different than if its fire
            string[] calls = { };
            for (int i = 0; i <= 40; i++)
            {
                if (this.roomList[i].sensor.sendState())
                { // Sensor is on
                    if (Gas == false)
                    {
                        this.roomList[i].sensor.alarmType = "Fire";
                        // only push once
                        if (Array.IndexOf(calls, "Fire Department") == -1)
                        {
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "On Call Operator";
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "Police";
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "Fire Department";
                        }

                    }
                    else if(Gas == true) {
                        // only push once
                        this.roomList[i].sensor.alarmType = "Gas";
                        if (Array.IndexOf(calls, "Gas Department") == -1) {
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "On Call Operator";
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "Gas Department";
                            Array.Resize(ref calls, calls.Length + 1);
                            calls[calls.Length - 1] = "Police";
                        }
                    }
                }
            }
            return calls;
        }
    }
}
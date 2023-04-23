/* This class handles turning on and off the alarm.
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{

    public class Alarm {

        public Alarm() {
        }

        public bool isActive = false; // Default alarm is off

        
        public bool activateAlarm() {
            // Active alarm signals the alarm to turn on
            this.isActive = true;
            return this.isActive;
        }

        public bool deactivateAlarm() {
            // Disable the alarm signals the alarm to turn off
            this.isActive = false;
            return this.isActive;
        }

    }
}
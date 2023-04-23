/* This class handles turning on and off direction indicators and getting its state
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class DirectionIndicators {

        public DirectionIndicators() {
        }

        public bool isActive = false;
        public bool turnOnIndicators() {
            // Turns on the direction indicators
            this.isActive = true;
            return this.isActive;
        }

        public bool turnOffIndicators() {
            // Turns  the direction indicators
            this.isActive = false;
            return this.isActive;
        }
    }
}
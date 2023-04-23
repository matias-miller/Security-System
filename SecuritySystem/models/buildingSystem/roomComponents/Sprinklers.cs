/*
 * This class handles turning on and off the spinkler.
 *
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Sprinklers {

        public Sprinklers() {
        }

        public bool isActive = false;

        public bool activateSprinkler() {
            // Activates the spinkler system
            this.isActive = true;
            return this.isActive;
        }

        public bool turnOffSprinkler() {
            // Turn off the spinkler system
            this.isActive = false;
            return this.isActive;
        }

    }
}
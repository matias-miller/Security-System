
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class Sprinklers {

        public Sprinklers() {
        }

        public bool isActive = false;

        public int sprinklerID = 0;

        /// <summary>
        /// @return
        /// </summary>
        public bool activateSprinkler() {
            // TODO implement here
            this.isActive = true;
            return this.isActive;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool turnOffSprinkler() {
            // TODO implement here
            this.isActive = false;
            return this.isActive;
        }

        /// <summary>
        /// Returns the sprinkler attribute variables
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return this.isActive;
        }

    }
}
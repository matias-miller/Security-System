
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.roomComponents{
    public class DirectionIndicators {

        public DirectionIndicators() {
        }

        public bool isActive = false;

        public int directionIndicatorID = 0;

        /// <summary>
        /// @return
        /// </summary>
        public bool turnOnIndicators() {
            // TODO implement here
            this.isActive = true;
            return this.isActive;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool turnOffIndicators() {
            // TODO implement here
            this.isActive = false;
            return this.isActive;
        }

        /// <summary>
        /// returns DirectionIndicators attributes
        /// @return
        /// </summary>
        public bool sendState() {
            // TODO implement here
            return this.isActive;
        }

    }
}
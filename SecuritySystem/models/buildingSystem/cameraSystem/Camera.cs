
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.cameraSystem{
    public class Camera {

        public Camera() {
        }

        /// <summary>
        /// index in cameraState
        /// </summary>
        private int cameraID;

        /// <summary>
        /// index in cameraState
        /// </summary>
        private bool peoplePresent;

        /// <summary>
        /// index in cameraState
        /// </summary>
        private bool tresspasserPresent;

        /// <summary>
        /// index in cameraState
        /// </summary>
        private bool firePresent;

        /// <summary>
        /// This holds cameraID, peoplePresent, tresspasserPresent, firePresent
        /// </summary>
        private object cameraState;

        /// <summary>
        /// in CameraSystem this is how the camera state will be fetched
        /// @return
        /// </summary>
        public object sendCameraStateToCameraSystem() {
            // TODO implement here
            return null;
        }

        /// <summary>
        /// Returns cameraID
        /// @return
        /// </summary>
        public int getCameraID() {
            // TODO implement here
            return 0;
        }

        /// <summary>
        /// updates, the camera attributes and stores them in camera state
        /// @return
        /// </summary>
        public bool updateCameraState() {
            // TODO implement here
            return false;
        }

    }
}
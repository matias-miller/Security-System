
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buildingSystem.cameraSystem{
    public class CameraSystem {

        public CameraSystem() {
        }

        /// <summary>
        /// holds a array of the different camera states
        /// </summary>
        private bool cameraStateArray = false;

        /// <summary>
        /// Called in returnCameraState, using the cameraStateArray.
        /// @return
        /// </summary>
        private bool detectTresspassersDuringOffHours() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Called in returnCameraState and in BuildingControlSystem to determine if doors can be locked or not, the specific function in BuildingControlSystem is requestToModifyBuildingState. It will check the cameraStateArray.
        /// @return
        /// </summary>
        public bool determineIfPeopleArePresent() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Called in monitorSystem, in a if statement to check if the camera state object fire is true
        /// @return
        /// </summary>
        private bool detectFireViaCamera() {
            // TODO implement here
            return false;
        }

        private void notifyControllCenterOnStatusChange() {
            // TODO implement here
        }

        /// <summary>
        /// Used to get state of specific camera. This will be called in the start of monitorSystem to update cameraState array, and will loop through all camera states.
        /// @return
        /// </summary>
        private bool requestCameraFeed() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// @return
        /// </summary>
        public bool confirmAlarm() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// This will be running in the background monitoring the camera arrays of the rooms.
        /// @return
        /// </summary>
        public bool monitorSystem() {
            // TODO implement here
            return false;
        }

        /// <summary>
        /// Allows for other systems such as BuildingControlSystem to query cameras
        /// @param cameraID 
        /// @return
        /// </summary>
        public bool returnCameraState(int cameraID) {
            // TODO implement here
            return false;
        }

    }
}
using System;
namespace SecuritySystem.Models
{
    public class CameraSystem
    {
        public string CameraState { get; set; }

        public void DetectTresspassersDuringOffHours() { }
        public void DetermineIfPeopleArePresent() { }
        public void DetectFireViaCamera() { }
        public void NotifyControlCenterOnStatusChange() { }
        public void RequestCameraFeed() { }
    }

    public class Camera
    {
        public void SendCameraFeedToCameraSystem() { }
    }

}


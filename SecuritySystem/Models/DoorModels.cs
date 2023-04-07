using System;
namespace SecuritySystem.Models
{
    public class Door
    {
        public bool IsLocked { get; set; }

        public void LockDoor() { }
        public void UnlockDoor() { }
        public void SendStateOnChange() { }
    }

}


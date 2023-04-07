using System;
namespace SecuritySystem.Models
{
    public class Sprinklers
    {
        public bool IsActive { get; set; }

        public void ActivateSprinkler() { }
        public void TurnOffSprinkler() { }
        public void SendStateOnChange() { }
    }

}


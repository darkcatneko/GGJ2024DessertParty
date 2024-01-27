using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Gamemanager
{
    public class PlayerMovementCommand : GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
        public Vector2 PlayerMovementVector;
    }
    public class PlayerVacuumControlCommand:GameEventMessageBase 
    {
        public PlayerIdentity PlayerIdentity;
        public Vector2 PlayerVacuumVector;
    }
    public class PlayerVacuumSwitchCommand:GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
        public bool Trigger;
    }
    public class PlayerShootTriggerCommand:GameEventMessageBase 
    {
        public PlayerIdentity PlayerIdentity; 
    }
}

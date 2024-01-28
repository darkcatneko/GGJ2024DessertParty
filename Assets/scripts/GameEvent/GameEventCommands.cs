using UnityEngine;

namespace Gamemanager
{
    public class PlayerMovementCommand : GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
        public Vector2 PlayerMovementVector;
    }
    public class PlayerVacuumControlCommand : GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
        public Vector2 PlayerVacuumVector;
    }
    public class PlayerVacuumSwitchCommand : GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
        public bool Trigger;
    }
    public class PlayerShootTriggerCommand : GameEventMessageBase
    {
        public PlayerIdentity PlayerIdentity;
    }

    public class PlayerGetIngredientCommand : GameEventMessageBase
    {
        public IngredientType IngredientType;
    }
    public class PlayerFinishQuestCommand : GameEventMessageBase
    {
        public PlayerIdentity FinishPlayerIdentity; 
    }

    public class GameTimeUpCommand:GameEventMessageBase
    {

    }
}

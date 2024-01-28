using Gamemanager;
using System;

public class MainGameEventPack : GameEventPack
{
    public IObservable<PlayerMovementCommand> OnPlayerMovement => getSubject<PlayerMovementCommand>();

    public IObservable<PlayerVacuumControlCommand> OnPlayerVacuumControl => getSubject<PlayerVacuumControlCommand>();

    public IObservable<PlayerVacuumSwitchCommand> OnPlayerVacuumSwitch => getSubject<PlayerVacuumSwitchCommand>();

    public IObservable<PlayerShootTriggerCommand> OnPlayerShootTrigger => getSubject<PlayerShootTriggerCommand>();

    public IObservable<PlayerGetIngredientCommand> OnPlayerGetIngredient => getSubject<PlayerGetIngredientCommand>();

    public IObservable<PlayerFinishQuestCommand> OnPlayerFinishQuest => getSubject<PlayerFinishQuestCommand>();

    public IObservable<GameTimeUpCommand> OnGameTimeUp => getSubject<GameTimeUpCommand>();

    public IObservable<GameQuestUIUpdateCommand> OnGameQuestUIUpdate => getSubject<GameQuestUIUpdateCommand>();
}

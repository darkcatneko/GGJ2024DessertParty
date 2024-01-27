using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamemanager;
using UniRx;
using System;

public class MainGameEventPack : GameEventPack
{
    public IObservable<PlayerMovementCommand> OnPlayerMovement => getSubject<PlayerMovementCommand>();

    public IObservable<PlayerVacuumControlCommand> OnPlayerVacuumControl=> getSubject<PlayerVacuumControlCommand>();

    public IObservable<PlayerVacuumSwitchCommand> OnPlayerVacuumSwitch => getSubject<PlayerVacuumSwitchCommand>();
}

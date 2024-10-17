using System;
using UnityEngine;

public class GameplayState : BaseState
{
    public static event Action OnObjectsActivated;

    public override void OnStart()
    {
        OnObjectsActivated?.Invoke();
        GameManager.Instance.PlayerController.Controls.Gameplay.Enable();

        if (!UpgradesManager.hasWeapon)
            GameManager.Instance.stateMachine.TransitionTo<UpgradeState>();
    }

    public override void OnEnd()
    {
        GameManager.Instance.PlayerController.Controls.Gameplay.Disable();
    }
}
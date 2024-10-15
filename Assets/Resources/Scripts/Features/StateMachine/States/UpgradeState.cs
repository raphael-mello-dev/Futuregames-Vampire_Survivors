using System;
using UnityEngine;

public class UpgradeState : BaseState
{
    public static event Action OnStateEntered;
    public override void OnStart()
    {
        OnStateEntered?.Invoke();
    }
}

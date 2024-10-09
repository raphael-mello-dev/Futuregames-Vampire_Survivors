using System;
using UnityEngine;

public class EndGameState : BaseState
{
    public static event Action<bool> OnObjectsDeactivated;
    public static event Action OnGameEnded;

    public override void OnStart()
    {
        OnObjectsDeactivated?.Invoke(false);
        OnGameEnded?.Invoke();
    }
}

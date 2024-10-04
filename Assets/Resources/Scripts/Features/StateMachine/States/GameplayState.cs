using UnityEngine;

public class GameplayState : BaseState
{
    public override void OnStart()
    {
        GameManager.Instance.PlayerController.Controls.Gameplay.Enable();
    }

    public override void OnEnd()
    {
        GameManager.Instance.PlayerController.Controls.Gameplay.Disable();
    }
}

using UnityEngine;

public class PlayerInputManager
{
    public Controls Controls { get; private set; }

    public Vector2 movement => Controls.Gameplay.Movement.ReadValue<Vector2>().normalized;

    public PlayerInputManager()
    {
        Controls = new Controls();
    }
}
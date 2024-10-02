using UnityEngine;

public class PlayerInputManager
{
    private Controls controls;

    public Vector2 movement => controls.Gameplay.Movement.ReadValue<Vector2>().normalized;

    public PlayerInputManager()
    {
        controls = new Controls();
        controls.Gameplay.Enable();
    }
}
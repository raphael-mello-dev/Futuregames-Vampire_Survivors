using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager
{
    public Controls Controls { get; private set; }

    public Vector2 movement => Controls.Gameplay.Movement.ReadValue<Vector2>().normalized;

    public event Action OnProjectileAttacked;

    public PlayerInputManager()
    {
        Controls = new Controls();

        Controls.Gameplay.Attack.performed += OnAttackPerformed;
    }

    void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnProjectileAttacked?.Invoke();
    }
}
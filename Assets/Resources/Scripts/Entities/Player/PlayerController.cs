using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]

public class PlayerController : MonoBehaviour
{
    public PlayerInputManager InputManager { get; private set; }

    void Start()
    {
        InputManager = new PlayerInputManager();
    }
}
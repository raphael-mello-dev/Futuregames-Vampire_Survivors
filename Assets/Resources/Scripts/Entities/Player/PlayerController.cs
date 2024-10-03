using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]

public class PlayerController : MonoBehaviour
{
    protected PlayerInputManager inputManager;

    void Start()
    {
        inputManager = new PlayerInputManager();
    }
}
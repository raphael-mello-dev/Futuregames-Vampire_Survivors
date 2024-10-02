using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]

public class PlayerController : MonoBehaviour
{
    protected PlayerInputManager inputManager;

    void Start()
    {
        inputManager = new PlayerInputManager();
    }
}
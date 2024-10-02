using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    protected PlayerInputManager inputManager;

    void Start()
    {
        inputManager = new PlayerInputManager();
    }
}
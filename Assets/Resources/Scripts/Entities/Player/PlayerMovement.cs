using UnityEngine;

public class PlayerMovement : PlayerController
{
    [SerializeField] private float speed;

    void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        transform.position += new Vector3(inputManager.movement.x, inputManager.movement.y, 0) * speed * Time.deltaTime;
    }
}

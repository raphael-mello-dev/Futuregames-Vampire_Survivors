using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        transform.position += new Vector3(GameManager.Instance.PlayerController.movement.x, GameManager.Instance.PlayerController.movement.y, 0) * speed * Time.deltaTime;
    }
}
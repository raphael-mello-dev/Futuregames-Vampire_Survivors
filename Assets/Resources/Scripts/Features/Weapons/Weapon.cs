using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BasicEnemy>().OnTakeDamage(attackDamage);
        }
    }
}

using UnityEngine;

public class BasicEnemy : EnemyBase
{
    void Awake()
    {
        identifier = "basicEnemy";

        EnemyConfig();
    }

    public override void OnTakeDamage(int damageAmount)
    {
        base.OnTakeDamage(damageAmount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var obj = other.gameObject;

        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerHealth>().OnTakeDamage(attack);
        }
    }
}
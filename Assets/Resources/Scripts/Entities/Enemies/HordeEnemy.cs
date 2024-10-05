using UnityEngine;

public class HordeEnemy : EnemyBase
{
    void Awake()
    {
        identifier = "hordeEnemy";

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
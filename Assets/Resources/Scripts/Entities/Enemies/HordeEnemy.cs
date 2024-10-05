using UnityEngine;

public class HordeEnemy : EnemyBase
{
    private DropsManager dropsManager;

    void Awake()
    {
        identifier = "hordeEnemy";

        EnemyConfig();
    }

    private void Start()
    {
        dropsManager = GameObject.FindObjectOfType<DropsManager>();
    }

    public override void OnTakeDamage(int damageAmount)
    {
        base.OnTakeDamage(damageAmount);
    }

    protected override void Death()
    {
        dropsManager.SpawnMediumXP(transform.position);
        base.Death();
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
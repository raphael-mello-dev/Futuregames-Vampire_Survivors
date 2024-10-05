using UnityEngine;

public class BasicEnemy : EnemyBase
{
    private DropsManager dropsManager;

    void Awake()
    {
        identifier = "basicEnemy";

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
        dropsManager.SpawnSmallXP(transform.position);
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
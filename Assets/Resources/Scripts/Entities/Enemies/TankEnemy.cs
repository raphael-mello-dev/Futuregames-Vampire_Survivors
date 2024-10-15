using UnityEngine;

public class TankEnemy : EnemyBase
{
    private HUDManager hudManager;
    private DropsManager dropsManager;

    void Awake()
    {
        identifier = "tankEnemy";
    }

    private void Start()
    {
        hudManager = GameObject.FindObjectOfType<HUDManager>();
        dropsManager = GameObject.FindObjectOfType<DropsManager>();
    }

    public override void OnTakeDamage(int damageAmount)
    {
        base.OnTakeDamage(damageAmount);
    }

    protected override void Death()
    {
        dropsManager.SpawnLargeXP(transform.position);
        base.Death();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var obj = other.gameObject;

        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerHealth>().OnTakeDamage(attack);
            hudManager.PlayerInfoDisplay(obj.GetComponent<PlayerLevel>().GetPlayerInfo());
        }
    }
}
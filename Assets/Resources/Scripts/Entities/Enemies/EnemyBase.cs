using System.Collections;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyDataSO enemyData;

    protected string identifier;
    protected int maxHealth;
    protected int health;
    protected float speed;
    protected int attack;

    public Transform Player {  get; private set; }
    private ParticlesManager ParticlesManager;

    [Header("Player Detection")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected ChaseType chaseType;
    private bool hasBeenDetected;

    public enum ChaseType
    {
        OnlyOnRadius,
        OnceDetected,
        AlwaysChase
    }

    protected void EnemyConfig()
    {
        enemyData = Resources.Load<EnemyDataSO>($"ScriptableObjects/Entities/Enemies/{identifier}");

        maxHealth = enemyData.maxHealth;
        health = enemyData.health;
        speed = enemyData.speed;
        attack = enemyData.attack;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        hasBeenDetected = false;
        ParticlesManager = GameObject.FindObjectOfType<ParticlesManager>();
    }

    public void OnMove()
    {
        switch (chaseType)
        {
            case ChaseType.OnlyOnRadius:
                if (DetectionRange())
                    OnChaseDown();
            break;
            case ChaseType.OnceDetected:
                if (!hasBeenDetected)
                    DetectionRange();
                else
                    OnChaseDown();
            break;
            case ChaseType.AlwaysChase:
                OnChaseDown();
            break;
        }
    }

    bool DetectionRange()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        if (playersInRange.Length > 0)
        {
            hasBeenDetected = true;
            return true;
        }
        else
            return false;
    }

    void OnChaseDown()
    {
        Vector3 dir = -(transform.position - Player.position);
        transform.position += dir * speed * Time.deltaTime;
    }

    public virtual void OnAttack() { }

    public virtual void OnTakeDamage(int damageAmount)
    {
        if (damageAmount >= health)
            Death();
        else
        {
            health -= damageAmount;
            StartCoroutine(nameof(DamageVisual));
        }
    }

    private IEnumerator DamageVisual()
    {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(139f, 0, 0);
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected virtual void Death()
    {
        health = enemyData.health;
        ParticlesManager.ActivateBloodPtl(transform.position);
        gameObject.SetActive(false);
    }
}
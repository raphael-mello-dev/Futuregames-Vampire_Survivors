using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyDataSO enemyData;

    protected string identifier;
    protected int maxHealth;
    protected int health;
    protected float speed;
    protected int attack;

    public enum ChaseType
    {
        OnlyOnRadius,
        OnceDetected,
        AlwaysChase
    }
    public Transform Player {  get; private set; }

    [Header("Player Detection")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected ChaseType chaseType;
    private bool hasBeenDetected;

    protected void EnemyConfig()
    {
        enemyData = Resources.Load<EnemyDataSO>($"ScriptableObjects/Entities/Enemies/{identifier}");

        maxHealth = enemyData.maxHealth;
        health = enemyData.health;
        speed = enemyData.speed;
        attack = enemyData.attack;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        hasBeenDetected = false;
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

    protected virtual void OnAttack()
    {

    }

    protected virtual void OnTakeDamage(int damageAmount)
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    void Start()
    {
        identifier = "basicEnemy";
        EnemyConfig();
    }

    void Update()
    {
        OnMove();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
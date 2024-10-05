using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data", fileName ="New Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public int maxHealth;
    public int health;
    [Range(0, 1.5f)]
    public float speed;
    public int attack;
}
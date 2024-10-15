using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;

    private void OnEnable()
    {
        attackDamage = 0;
    }
}
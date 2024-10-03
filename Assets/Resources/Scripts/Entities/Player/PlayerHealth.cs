using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    public void OnTakeDamage(int damageAmount)
    {
        if (health > 1)
        {
            StartCoroutine(nameof(DamageVisual));
            health -= damageAmount;
        }
        else
            gameObject.SetActive(false);

    }

    private IEnumerator DamageVisual()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

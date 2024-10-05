using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

    public float maxHealth;
    public float health;

    public void OnTakeDamage(int damageAmount)
    {
        if ((health - damageAmount) > 0)
        {
            StartCoroutine(nameof(DamageVisual));
            health -= damageAmount;
            hudManager.LifeBarDisplay((health/maxHealth));
        }
        else
        {
            hudManager.LifeBarDisplay(0);
            gameObject.SetActive(false);
        }

    }

    private IEnumerator DamageVisual()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

    public float maxHealth;
    public float health;

    void OnDisable()
    {
        maxHealth = 10;
        health = maxHealth;
        hudManager.LifeBarDisplay((health/maxHealth));
        GetComponent<SpriteRenderer>().color = Color.white;
    }

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
            health = 0;
            hudManager.LifeBarDisplay(0);
            hudManager.PlayerInfoDisplay(gameObject.GetComponent<PlayerLevel>().GetPlayerInfo());
            gameObject.SetActive(false);
            GameManager.Instance.stateMachine.TransitionTo<EndGameState>();
        }

    }

    private IEnumerator DamageVisual()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
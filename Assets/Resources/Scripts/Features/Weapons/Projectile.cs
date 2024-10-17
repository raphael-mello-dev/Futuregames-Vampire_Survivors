using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;

    [SerializeField] private float speed;

    void Start()
    {
        GetPointerPosition();
        Invoke("HideProjectile", 4);
    }

    void Update()
    {
        OnMove();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBase>().OnTakeDamage(GameObject.FindFirstObjectByType<Weapon>().TotalDamage);
            gameObject.SetActive(false);
        }
    }

    void GetPointerPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        direction = (mousePosition - transform.position).normalized;
    }

    void OnMove()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void HideProjectile()
    {
        gameObject.SetActive(false);
    }
}

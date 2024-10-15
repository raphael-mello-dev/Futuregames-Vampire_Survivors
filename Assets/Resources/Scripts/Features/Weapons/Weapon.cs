using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public UpgradeSO.AttachedType aType;

    public int attackDamage;

    private GameObject parent;

    [SerializeField] private float distanceFromPlayer = 1.5f;
    [SerializeField] private float rotationSpeedPjtls = 20.0f;
    [SerializeField] private float rotationSpeedOrbits = 40.0f;
    [SerializeField] private float offsetPjtls = -90f;
    [SerializeField] float offsetOrbits = -270f;

    private float angle;

    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("Player");
    }

    void OnDisable()
    {
       Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.Instance.stateMachine.currentState.ToString() == "GameplayState")
            AttackMode();
    }

    void AttackMode()
    {
        switch (aType)
        {
            case UpgradeSO.AttachedType.Projectiles:
                ProjectileWeaponMove();
                break;
            case UpgradeSO.AttachedType.OrbitsAround:
                OrbitWeaponMove();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var totalDamage = parent.GetComponent<PlayerAttack>().attackDamage + attackDamage;
            other.GetComponent<EnemyBase>().OnTakeDamage(totalDamage);
        }
    }

    private void OrbitWeaponMove()
    {
        angle += rotationSpeedOrbits * Time.deltaTime; // Increment angle
        float radian = angle * Mathf.Deg2Rad; // Convert angle to radians

        // Calculate the new position based on the angle
        float x = parent.transform.position.x + Mathf.Cos(radian) * distanceFromPlayer;
        float y = parent.transform.position.y + Mathf.Sin(radian) * distanceFromPlayer;

        // Update the weapon's position
        transform.position = new Vector3(x, y, transform.position.z);

        // Point the weapon at the player
        PointWeaponAtPlayer();
    }

    private void PointWeaponAtPlayer()
    {
        Vector3 direction = parent.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offsetOrbits));
    }

    private void ProjectileWeaponMove()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - parent.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 desiredPosition = parent.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * distanceFromPlayer;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeedPjtls * Time.deltaTime);
        PointWeaponAtCursor(mousePosition);
    }

    private void PointWeaponAtCursor(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offsetPjtls));
    }
}


//using System;
//using UnityEngine;

//public class Weapon : MonoBehaviour
//{
//    public UpgradeSO.AttachedType aType;

//    public int attackDamage;

//    private GameObject parent;

//    [SerializeField] private float distanceFromPlayer = 1.5f;
//    [SerializeField] private float rotationSpeed = 20.0f;
//    [SerializeField] private float offset = -90f;

//    private void Start()
//    {
//        parent = GameObject.FindGameObjectWithTag("Player");
//    }

//    void OnDisable()
//    {
//        attackDamage = 10;
//    }

//    void Update()
//    {
//        if (GameManager.Instance.stateMachine.currentState.ToString() == "GameplayState")
//            AttackMode();
//    }

//    void AttackMode()
//    {

//        switch (aType)
//        {
//            case UpgradeSO.AttachedType.Projectiles:
//                ProjectileWeaponMove();
//                break;
//            case UpgradeSO.AttachedType.OrbitsAround:
//                //OnOrbitAttacked?.Invoke();
//                break;
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Enemy"))
//        {
//            var totalDamage = parent.GetComponent<PlayerAttack>().attackDamage + attackDamage;
//            other.GetComponent<EnemyBase>().OnTakeDamage(totalDamage);
//        }
//    }
//    //private void OrbitWeaponMove()
//    //{
//    //    Vector3 direction = parent.transform.position - transform.position;
//    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//    //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

//    //    //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//    //    //mousePosition.z = 0;

//    //    //Vector3 direction = (mousePosition - parent.transform.position).normalized;
//    //    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//    //    //Vector3 desiredPosition = parent.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * distanceFromPlayer;
//    //    //transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
//    //    //PointWeaponAtCursor(mousePosition);
//    //}

//    private void ProjectileWeaponMove()
//    {
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0;

//        Vector3 direction = (mousePosition - parent.transform.position).normalized;
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        Vector3 desiredPosition = parent.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * distanceFromPlayer;
//        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
//        PointWeaponAtCursor(mousePosition);
//    }

//    private void PointWeaponAtCursor(Vector3 mousePosition)
//    {
//        Vector3 direction = mousePosition - transform.position;
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
//    }
//}
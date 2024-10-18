using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public string weaponName;
    [HideInInspector] public UpgradeSO.AttachedType aType;

    public int attackDamage;
    public int TotalDamage { get { return parent.GetComponent<PlayerAttack>().attackDamage + attackDamage; } }

    private GameObject parent;
    public GameObject prefabParent;

    [SerializeField] private float distanceFromPlayer = 1.5f;
    [SerializeField] private float rotationSpeedPjtls = 20.0f;
    [SerializeField] private float offsetPjtls = -90f;

    private float angle;

    public static event Action OnOrbitAttacked;

    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("Player");

        if (weaponName == "Normal Sword")
            FindObjectOfType<WeaponParent>().swordWeapons.Add(this);
    }

    private void OnEnable()
    {
        if (weaponName == "Normal Mace")
            GameObject.FindObjectOfType<ProjectilesManager>().SetShotPoint(GameObject.FindGameObjectWithTag("ShotPoint").GetComponent<Transform>());
    }

    void OnDisable()
    {
       Destroy(prefabParent);
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
                OnOrbitAttacked?.Invoke();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().OnTakeDamage(TotalDamage);
        }
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
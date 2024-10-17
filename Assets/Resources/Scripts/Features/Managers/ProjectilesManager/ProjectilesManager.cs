using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> projectilesList;
    [SerializeField] private int capacity;

    [SerializeField] private Transform shotPoint;

    [SerializeField] private GameObject player;

    [SerializeField] private bool hasProjectileWeapon;

    void Start()
    {
        hasProjectileWeapon = false;

        for (int i = 0; i < capacity; i++)
        {
            GameObject projectile = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/MagicProjectile"), Vector3.zero, Quaternion.identity, gameObject.transform);
            projectile.SetActive(false);
            projectilesList.Add(projectile);
        }
    }

    private void OnEnable()
    {
        GameplayState.OnObjectsActivated -= CheckingObjEnable;
        EndGameState.OnObjectsDeactivated += gameObject.SetActive;
        
        if (GameManager.Instance.stateMachine.currentState.ToString() == "GameplayState")
            GameManager.Instance.PlayerController.OnProjectileAttacked += SpawnProjectile;
    }

    private void OnDisable()
    {
        hasProjectileWeapon = false;
        GameplayState.OnObjectsActivated += CheckingObjEnable;
        EndGameState.OnObjectsDeactivated -= gameObject.SetActive;

        if (GameManager.Instance.stateMachine.currentState.ToString() == "EndGameState")
            GameManager.Instance.PlayerController.OnProjectileAttacked -= SpawnProjectile;
    }

    public void SetShotPoint(Transform reference)
    {
        shotPoint = reference;
    }

    void SpawnProjectile()
    {
        if(hasProjectileWeapon || HasProjectileWeaponAttached())
        {
            foreach (GameObject projectile in projectilesList)
            {
                if (!projectile.activeInHierarchy)
                {
                    projectile.transform.position = shotPoint.position;
                    projectile.SetActive(true);
                    return;
                }
            }
        }
    }

    bool HasProjectileWeaponAttached()
    {
        Weapon[] weapons = player.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponName == "Normal Mace")
            {
                hasProjectileWeapon = true;
                return true;
            }
        }

        return false;
    }

    void CheckingObjEnable()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
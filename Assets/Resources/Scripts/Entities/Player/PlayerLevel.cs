using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

    public static event Action OnLeveledUp;

    public int Level { get; private set; }
    public float NeededXP { get; private set; }
    public float CurrentXP { get; set; }
    
    private float increaseXPRate;

    private void Start()
    {
        Level = 0;
        CurrentXP = 0;
        increaseXPRate = 0.2f;
        NeededXP = (100 * (1 + increaseXPRate));

        hudManager.PlayerInfoDisplay(GetPlayerInfo());
    }

    private void OnEnable()
    {
        GameplayState.OnObjectsActivated -= CheckingObjEnable;
        Upgrade.OnEfectGiven += GetUpgrade;
    }

    private void OnDisable()
    {
        GameplayState.OnObjectsActivated += CheckingObjEnable;
        Upgrade.OnEfectGiven -= GetUpgrade;


        if (Level > GameManager.Instance.topScore)
            GameManager.Instance.topScore = Level;

        Level = 0;
        CurrentXP = 0;
        increaseXPRate = 0.2f;
        NeededXP = (100 * (1 + increaseXPRate));

        UpgradesManager.hasWeapon = false;
    }

    public void LevelUp()
    {
        Level++;
        increaseXPRate += 0.2f * Level;
        NeededXP = (100 * (1 + increaseXPRate));
        hudManager.LevelTextUpdate(Level);
        OnLeveledUp?.Invoke();
        GameManager.Instance.stateMachine.TransitionTo<UpgradeState>();
    }

    public void XPIncrease(float value)
    {
        if ((CurrentXP + value) >= NeededXP)
        {
            CurrentXP += (value - NeededXP);
            LevelUp();
            hudManager.XPBarDisplay(XPBarCalc());
        }
        else
        {
            CurrentXP += value;
            hudManager.XPBarDisplay(XPBarCalc());
        }
    }

    float XPBarCalc()
    {
        return (CurrentXP / NeededXP);
    }

    int RandomUpgrade()
    {
        var number = UnityEngine.Random.Range(0, 2f);
        return (int)(Mathf.Round(number));
    }

    void GetUpgrade(UpgradeSO reference)
    {
        if (reference.type == UpgradeSO.UpgradeType.AddStats)
        {
            switch (reference.statType)
            {
                case UpgradeSO.StatType.Health:
                    gameObject.GetComponent<PlayerHealth>().maxHealth += reference.value;
                    gameObject.GetComponent<PlayerHealth>().health += reference.value;
                    break;
                case UpgradeSO.StatType.Speed:
                    gameObject.GetComponent<PlayerMovement>().speed += reference.value;
                    break;
                case UpgradeSO.StatType.Damage:
                    gameObject.GetComponent<PlayerAttack>().attackDamage += (int)reference.value;
                    break;
            }
        }
        else
        {
            switch (reference.attachedType)
            {
                case UpgradeSO.AttachedType.NotAttachable:
                    break;
                default:
                    GameObject parent = FindObjectOfType<WeaponParent>().gameObject;
                    GameObject prefab = Resources.Load<GameObject>(reference.PrefabPath);
                    prefab.GetComponentInChildren<Weapon>().weaponName = reference.nameUpgd;
                    prefab.GetComponentInChildren<Weapon>().aType = reference.attachedType;
                    prefab.GetComponentInChildren<Weapon>().attackDamage = (int)reference.value;
                    Instantiate(prefab, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity, parent.transform);
                break;
            }
        }

        hudManager.PlayerInfoDisplay(GetPlayerInfo());
    }

    public string GetPlayerInfo()
    {
        return $"HP: {gameObject.GetComponent<PlayerHealth>().health}/{gameObject.GetComponent<PlayerHealth>().maxHealth} - " +
            $"Speed: {gameObject.GetComponent<PlayerMovement>().speed} - Player Power: {gameObject.GetComponent<PlayerAttack>().attackDamage}";
    }

    void CheckingObjEnable()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
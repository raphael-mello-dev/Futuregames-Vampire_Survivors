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
    }

    private void OnDisable()
    {
        GameplayState.OnObjectsActivated += CheckingObjEnable;

        if (Level > GameManager.Instance.topScore)
            GameManager.Instance.topScore = Level;

        Level = 0;
        CurrentXP = 0;
        increaseXPRate = 0.2f;
        NeededXP = (100 * (1 + increaseXPRate));
    }

    public void LevelUp()
    {
        Level++;
        increaseXPRate += 0.2f * Level;
        NeededXP = (100 * (1 + increaseXPRate));
        hudManager.LevelTextUpdate(Level);
        GetUpgrade(RandomUpgrade());
        hudManager.PlayerInfoDisplay(GetPlayerInfo());
        OnLeveledUp?.Invoke();
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

    void GetUpgrade(int value)
    {
        switch (value)
        {
            case 0:
                gameObject.GetComponent<PlayerHealth>().maxHealth += 2;
                gameObject.GetComponent<PlayerHealth>().health += 2;
            break;
            case 1:
                gameObject.GetComponent<PlayerMovement>().speed++;
                break;
            case 2:
                gameObject.GetComponentInChildren<Weapon>().attackDamage++;
                break;
        }
    }

    public string GetPlayerInfo()
    {
        return $"HP: {gameObject.GetComponent<PlayerHealth>().health}/{gameObject.GetComponent<PlayerHealth>().maxHealth} - " +
            $"Speed: {gameObject.GetComponent<PlayerMovement>().speed} - Power: {gameObject.GetComponentInChildren<Weapon>().attackDamage}";
    }

    void CheckingObjEnable()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
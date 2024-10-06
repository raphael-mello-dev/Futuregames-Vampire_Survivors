using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

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
    }

    public void LevelUp()
    {
        Level++;
        increaseXPRate += 0.2f * Level;
        NeededXP = (100 * (1 + increaseXPRate));
        hudManager.LevelTextUpdate(Level);
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
}
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        LevelTextUpdate(0);
    }

    public void LevelTextUpdate(int level)
    {
        levelText.text = $"Level: {level}";
    }

    public void XPBarDisplay(float lifeAmount)
    {
        xpBar.fillAmount = lifeAmount;
    }

    public void LifeBarDisplay(float lifeAmount)
    {
        lifeBar.fillAmount = lifeAmount;
    }
}
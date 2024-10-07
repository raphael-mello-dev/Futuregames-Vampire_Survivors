using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI infoText;

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

    public void PlayerInfoDisplay(string text)
    {
        infoText.text = text;
    }
}
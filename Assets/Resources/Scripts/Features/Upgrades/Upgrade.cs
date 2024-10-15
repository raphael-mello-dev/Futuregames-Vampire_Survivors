using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UpgradeSO currentUpgrade;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private TextMeshProUGUI cardStat;
    [SerializeField] private Image cardSprite;
    private string stat;

    public static event Action OnUpgradeClicked;
    public static event Action<UpgradeSO> OnEfectGiven;

    private void OnEnable()
    {
        if (currentUpgrade != null)
        {
            cardName.text = currentUpgrade.nameUpgd;
            cardDescription.text = currentUpgrade.description;
            cardSprite.sprite = currentUpgrade.sprite;
            cardStat.text = $"{currentUpgrade.statType}: +{currentUpgrade.value}";
        }

        gameObject.GetComponent<Button>().onClick.AddListener(ChosenUpgrade);
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(ChosenUpgrade);
    }

    void GetUpgrade(UpgradeSO reference)
    {
        currentUpgrade = reference;
    }

    void ChosenUpgrade()
    {
        OnUpgradeClicked?.Invoke();
        OnEfectGiven?.Invoke(currentUpgrade);
        GameManager.Instance.stateMachine.TransitionTo<GameplayState>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradesPanel;

    [SerializeField] private UpgradeSO[] upgradesList;
    [SerializeField] private List<GameObject> currentUpgrades;

    [SerializeField] private int[] num;
    private float listLength;

    void Start()
    {
        OnExitUpgrades();
        
        upgradesList = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Features/Upgrades/");
        
        listLength = (upgradesList.Length - 1);

        for (int i = 0; i < num.Length; i++)
            num[i] = (int)Mathf.Round(Random.Range(0, listLength));
    }

    private void OnEnable()
    {
        UpgradeState.OnStateEntered += OnEnterUpgrades;
        Upgrade.OnUpgradeClicked += OnExitUpgrades;
    }

    private void OnDisable()
    {
        UpgradeState.OnStateEntered -= OnEnterUpgrades;
        Upgrade.OnUpgradeClicked -= OnExitUpgrades;
    }

    void OnEnterUpgrades()
    {
        int i = 0;

        foreach (var upgrade in currentUpgrades)
        {
            upgrade.GetComponent<Upgrade>().currentUpgrade = upgradesList[num[i]];
            i++;
        }

        upgradesPanel.SetActive(true);
    }

    void OnExitUpgrades()
    {
        for (int i = 0; i < num.Length; i++)
            num[i] = (int)Mathf.Round(Random.Range(0, listLength));

        upgradesPanel.SetActive(false);
    }
}
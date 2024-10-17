using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject firstUpgradePanel;
    [SerializeField] private GameObject firstUpgrade;

    [SerializeField] private UpgradeSO[] upgrades;
    [SerializeField] private List<UpgradeSO> upgradesList;
    [SerializeField] private List<GameObject> currentUpgrades;

    [SerializeField] private int[] num;
    public float ListLength { get { return (upgradesList.Count - 1); } }

    public static bool hasWeapon;

    void Start()
    {
        hasWeapon = false;

        OnExitUpgrades();

        upgrades = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Features/Upgrades/");

        upgradesList = upgrades.ToList();

        num = new int[currentUpgrades.Count];

        for (int i = 0; i < num.Length; i++)
            num[i] = UnityEngine.Random.Range(0, upgradesList.Count);
    }



    private void OnEnable()
    {
        UpgradeState.OnStateEntered += OnEnterUpgrades;
        Upgrade.OnUpgradeClicked += OnExitUpgrades;
        Upgrade.OnUpgradeRemoved += RemoveUpgrade;
        EndGameState.OnGameEnded += OnGameRestart;
    }

    private void OnDisable()
    {
        UpgradeState.OnStateEntered -= OnEnterUpgrades;
        Upgrade.OnUpgradeClicked -= OnExitUpgrades;
        Upgrade.OnUpgradeRemoved -= RemoveUpgrade;
        EndGameState.OnGameEnded -= OnGameRestart;
    }
    void OnEnterUpgrades()
    {
        if (hasWeapon)
        {
            int i = 0;

            foreach (var upgrade in currentUpgrades)
            {
                if (i < num.Length && num[i] < upgradesList.Count)
                    upgrade.GetComponent<Upgrade>().currentUpgrade = upgradesList[num[i]];

                i++;
            }

            upgradesPanel.SetActive(true);
        }
        else
        {
            foreach (var upgrade in upgradesList)
                firstUpgrade.GetComponent<Upgrade>().currentUpgrade = upgrade;

            firstUpgradePanel.SetActive(true);
            hasWeapon = true;
        }
    }

    void OnExitUpgrades()
    {
        for (int i = 0; i < num.Length; i++)
            num[i] = (int)Mathf.Round(UnityEngine.Random.Range(0, ListLength));

        upgradesPanel.SetActive(false);

        if (firstUpgradePanel.activeInHierarchy)
            firstUpgradePanel.SetActive(false);
    }

    void RemoveUpgrade(UpgradeSO reference)
    {
        if (upgradesList.Contains(reference))
        {
            upgradesList.Remove(reference);
        }   
    }

    void OnGameRestart()
    {
        upgradesList.Clear();
        upgradesList = upgrades.ToList();
    }
}
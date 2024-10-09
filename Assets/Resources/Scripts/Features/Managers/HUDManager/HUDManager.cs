using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Image lifeBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI infoText;

    private void OnEnable()
    {
        GameplayState.OnObjectsActivated -= CheckingObjEnable;
        EndGameState.OnObjectsDeactivated += gameObject.SetActive;

        gameplayPanel.SetActive(true);
        pausePanel.SetActive(true);

        LevelTextUpdate(0);
        XPBarDisplay(0);
    }

    private void OnDisable()
    {
        GameplayState.OnObjectsActivated += CheckingObjEnable;
        EndGameState.OnObjectsDeactivated -= gameObject.SetActive;

        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
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

    void CheckingObjEnable()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
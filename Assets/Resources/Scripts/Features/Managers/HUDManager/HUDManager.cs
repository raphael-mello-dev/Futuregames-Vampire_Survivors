using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image lifeBar;

    public void LifeBarDisplay(float lifeAmount)
    {
        lifeBar.fillAmount = lifeAmount;
    }
}
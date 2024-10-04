using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Buttons")]
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button languagesButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;

    [Header("Texts")]
    [SerializeField] private GameObject controlsText;
    [SerializeField] private GameObject creditsText;

    void Start()
    {
        controlsButton.onClick.AddListener(OnClickControls);
        languagesButton.onClick.AddListener(OnClickLanguages);
        creditsButton.onClick.AddListener(OnClickCredits);
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnDisable()
    {
        controlsText.SetActive(false);
        creditsText.SetActive(false);
    }

    void OnClickControls()
    {
        controlsText.SetActive(true);
        creditsText.SetActive(false);
    }

    void OnClickLanguages()
    {

    }

    void OnClickCredits()
    {
        controlsText.SetActive(false);
        creditsText.SetActive(true);
    }

    void OnClickBack()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}

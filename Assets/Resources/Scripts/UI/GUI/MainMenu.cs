using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject menuCanvas;

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject scoresPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button scoresButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlay);
        scoresButton.onClick.AddListener(OnClickScores);
        optionsButton.onClick.AddListener(OnClickOptions);
        exitButton.onClick.AddListener(OnClickExit);
    }

    void OnClickPlay()
    {
        menuCanvas.SetActive(false);
    }

    void OnClickScores()
    {
        menuPanel.SetActive(false);
        scoresPanel.SetActive(true);
    }

    void OnClickOptions()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
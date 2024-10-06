using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject GameplayCanvas;

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject scoresPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button scoresButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private MenuDoTween menuDoTween;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlay);
        scoresButton.onClick.AddListener(OnClickScores);
        optionsButton.onClick.AddListener(OnClickOptions);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnEnable()
    {
        menuPanel.SetActive(true);
        GameplayCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        menuPanel.SetActive(false);
        GameplayCanvas.SetActive(true);
    }

    void OnClickPlay()
    {
        GameManager.Instance.stateMachine.TransitionTo<GameplayState>();
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
        menuDoTween.MainMenuHide();
        menuDoTween.OptionsDoTween();
    }

    void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
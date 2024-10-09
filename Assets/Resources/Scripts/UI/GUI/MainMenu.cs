using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject GameplayCanvas;

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private MenuDoTween menuDoTween;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlay);
        optionsButton.onClick.AddListener(OnClickOptions);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnEnable()
    {
        scoreText.text = $"Hightest Level: {GameManager.Instance.topScore}";
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
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;

        pauseButton.onClick.AddListener(OnClickPause);
    }

    void OnClickPause()
    {
        if (!isPaused)
            GameManager.Instance.stateMachine.TransitionTo<PausedState>();
        else
            GameManager.Instance.stateMachine.TransitionTo<GameplayState>();

        isPaused = !isPaused;
    }
}
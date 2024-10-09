using System.Collections;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] private float EndScreenTime;

    private void OnEnable()
    {
        GameplayState.OnObjectsActivated += DeactivateEndGame;
        EndGameState.OnObjectsDeactivated -= ActivateEndGame;
        EndGameState.OnGameEnded += GameEnd;
    }

    private void OnDisable()
    {
        GameplayState.OnObjectsActivated -= DeactivateEndGame;
        EndGameState.OnObjectsDeactivated += ActivateEndGame;
        EndGameState.OnGameEnded -= GameEnd;
    }

    void GameEnd()
    {
        StartCoroutine("EndGameScreen");
    }

    IEnumerator EndGameScreen()
    {
        yield return new WaitForSeconds(EndScreenTime);
        GameManager.Instance.stateMachine.TransitionTo<MenuState>();
        gameObject.SetActive(false);
    }


    void ActivateEndGame(bool value)
    {
        gameObject.SetActive(!value);
    }
    void DeactivateEndGame()
    {
        gameObject.SetActive(false);
    }
}

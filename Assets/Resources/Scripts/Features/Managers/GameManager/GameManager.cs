using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public StateMachine stateMachine {  get; private set; }

    public GameObject MenuCanvas {  get; private set; }

    public PlayerInputManager PlayerController { get; private set; }

    public int topScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        MenuCanvas = GameObject.FindGameObjectWithTag("Canvas").gameObject;
        PlayerController = new PlayerInputManager();

        stateMachine = new StateMachine();
        stateMachine.TransitionTo<MenuState>();

        topScore = 0;
    }

    private void Update()
    {
        stateMachine.OnTick();
    }
}
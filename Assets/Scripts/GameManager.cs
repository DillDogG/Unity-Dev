using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject titleUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] Slider healthUI;
    [SerializeField] FloatVariable health;
    [Header("Events")]
    [SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent gameStartEvent;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER
    }

    public State state = State.TITLE;
    public float timer = 0;
    public int lives = 0;

    public float Timer
    {
        get { return timer; }
        set
        {
            timer = value;
            timerUI.text = timer.ToString();
        }
    }

    public int Lives {
        get { return lives; }
        set {
            lives = value;
            livesUI.text = "Lives: " + lives.ToString();
        }
    }

    private void OnEnable()
    {
        scoreEvent.Subscribe(OnAddPoints);
    }

    private void OnDisable()
    {
        scoreEvent.Unsubscribe(OnAddPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TITLE:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                titleUI.SetActive(false);
                timer = 60;
                lives = 3;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameStartEvent.RaiseEvent();
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:
                Timer = Timer - Time.deltaTime;
                break;
            case State.GAME_OVER:
                break;
            default:
                break;
        }

        healthUI.value = health.value / 100.0f;
    }

    public void OnStartGame()
    {
        state = State.TITLE;
    }

    public void OnAddPoints(int points)
    {
        print(points);
    }
}

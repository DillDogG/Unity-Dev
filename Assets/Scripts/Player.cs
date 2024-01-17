using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private int score = 0;
    [SerializeField] FloatVariable health;
    [SerializeField] PhysicsCharacterController characterController;
    [Header("Events")]
    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;

    private void Start()
    {
        health.value = 50;
    }

    public int Score {
        get { return score; }
        set {
            score = value;
            scoreText.text = "Score: " + score.ToString();
            scoreEvent.RaiseEvent(score);
        }
    }

    private void OnEnable()
    {
        gameStartEvent.Subscribe(OnStartGame);
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    private void OnStartGame()
    {
        characterController.enabled = true;
    }
}

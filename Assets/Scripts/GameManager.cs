using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] GameObject titleUI;
	[SerializeField] GameObject gameOverUI;
	[SerializeField] TMP_Text endTextUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] TMP_Text timerUI;
	[SerializeField] Slider healthUI;

	[SerializeField] FloatVariable health;
	[SerializeField] GameObject respawn;

	[Header("Events")]
	[SerializeField] Event gameStartEvent;
	[SerializeField] Event titleStartEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public enum State
	{
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER,
		GAME_LOST
	}

	private State state = State.TITLE;
	private float timer = 0;
	private int lives = 0;
	private int gameCount = 0;

	public int Lives { 
		get { return lives; } 
		set { 
			lives = value;
			livesUI.text = "LIVES: " + lives.ToString(); 
			} 
		}

	public float Timer
	{
		get { return timer; }
		set
		{
			timer = value;
			timerUI.text = string.Format("{0:F1}", timer);
		}
	}

    void Update()
	{
		switch (state)
		{
			case State.TITLE:
                gameOverUI.SetActive(false);
				titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
                Timer = 15;
                Lives = 3;
                break;
			case State.START_GAME:
				titleUI.SetActive(false);
                health.value = 100;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				gameStartEvent.RaiseEvent();
				respawnEvent.RaiseEvent(respawn);
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				Timer = Timer - Time.deltaTime;
				if (Timer <= 0)
				{
					state = State.GAME_OVER;
				}
				break;
			case State.GAME_OVER:
				gameOverUI.SetActive(true);
				endTextUI.text = "Game Over. Game count: " + (gameCount + 1).ToString();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
			case State.GAME_LOST:
                gameOverUI.SetActive(true);
				endTextUI.text = "You lost. Game count: " + (gameCount + 1).ToString();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
			default:
				break;
		}

		healthUI.value = health / 100.0f;
	}

	public void OnStartGame()
	{
		state = State.START_GAME;
	}

	public void OnGameEnd()
	{
		gameCount++;
		state = State.TITLE;
	}

	public void OnPlayerDead()
	{
		Lives--;
		if (Lives > 0) state = State.START_GAME;
		else state = State.GAME_LOST;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}
}

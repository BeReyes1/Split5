using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameState startingState;
    public GameState State { get; private set; }
    public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(startingState);
    }

    public void UpdateGameState(GameState newState)
    {
        if (newState == State) return;

        State = newState;

        switch (newState)
        {
            case GameState.Intro:
                break;
            case GameState.Countdown:
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }
}

public enum GameState
{
    None,
    Intro,
    Countdown,
    Playing,
    GameOver
}

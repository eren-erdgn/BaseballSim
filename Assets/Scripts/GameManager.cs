using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _state;

    private void Awake()
    {
        Instance = this;
    }

    public GameState CurrentState => _state;

    private void Start()
    {
        ChangeState(GameState.Start);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CurrentState != GameState.Playing) 
        {
            Events.OnGameStart.Invoke();
            ChangeState(GameState.Playing);
        }
    }


    public void ChangeState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {
            case GameState.Start:
                break;
            case GameState.Playing:
                break;
            case GameState.End:
                Events.OnGameEnd.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    Start,
    Playing,
    End
}
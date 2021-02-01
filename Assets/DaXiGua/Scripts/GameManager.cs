using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        WaitForStart,
        Gaming,
        GameOver
    }

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance;


    public GameState MyGameState
    {
        get
        {
            return gameState;
        }

        set
        {
            var lastState = gameState;
            gameState = value;
            StateChange();
            if (OnStateChange != null)
            {
                GameStateEventArgs e = new GameStateEventArgs();
                e.lastState = lastState;
                e.curState = gameState;
                OnStateChange(this, e);
            }
        }
    }
    public delegate void GameStateAction(object sender, EventArgs e);
    public static event GameStateAction OnStateChange;

    public class GameStateEventArgs : EventArgs
    {
        public GameState lastState;
        public GameState curState;
    }


    private GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            throw new UnityException("已有实例：" + name);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        MyGameState = GameState.Gaming;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameOver()
    {
        FruitSpawnController.Instance.ClearFruit();

    }

    void GameStart()
    {
        FruitSpawnController.Instance.SpawnFruit();
        ScoreContro.Instance.Restart();
    }

    void StateChange()
    {

        switch (gameState)
        {
            case GameState.WaitForStart:
                //WaitForStartFunc();
                break;
            case GameState.Gaming:
                 GameStart();
                break;
            case GameState.GameOver:
                 GameOver();
                break;
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public enum GameState { MENU,PLAYING, LOSE,WIN}
    public enum Level { PAST, FUTURE}

    private GameState state;
    private Level currentLevel = Level.PAST;
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void updateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.MENU:
                break;
            case GameState.PLAYING:
                break;
            case GameState.LOSE:
                break;
            case GameState.WIN:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    public String nextLevel(Vector3 p,Quaternion r)
    {
        // store player position
        playerPosition = p;
        playerRotation = r;

        if (currentLevel == Level.PAST)
        {
            currentLevel = Level.FUTURE;
            return "test_future"; // future scene name
        }
        else
        {
            currentLevel = Level.PAST;
            return "test_past"; // past scene name
        }
    }

    

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
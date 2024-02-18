using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public enum GameState { MENU,PLAYING, LOSE,WIN}
    public enum Level { PAST, FUTURE}

    private GameState state;
    public Level currentLevel;
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    public int NumberOfEggs = 28;
    public int nthPast = 0;

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


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "past")
        {
            currentLevel = Level.PAST;
        }
        else
        {
            currentLevel = Level.FUTURE;
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
            return "futuretesting"; // future scene name
        }
        else
        {
            nthPast++;
            Debug.Log(nthPast);
            
            currentLevel = Level.PAST;
            return "past"; // past scene name
        }
    }

    

   

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
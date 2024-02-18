using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioSource source;
    public void Play()
    {

        SceneManager.LoadScene("past");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        GameManager.instance.NumberOfEggs = 28;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}


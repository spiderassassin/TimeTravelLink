using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour

{
    public GameObject transitionVolume;

    [SerializeField] TextMeshProUGUI timerText;
    float timeLimit = 10f;
    float remainingTime;
    public float transitionTime = 3f;


    private GameObject player;
    private CharacterController controller;
    private Transform trans;


    public AudioClip past, future,glitch;
    private bool playedAlready = false;

 


    // Start is called before the first frame update
    void Start()
    { 
        remainingTime = timeLimit;
        moveToPrevPosition();
        if (GameManager.instance.currentLevel == GameManager.Level.PAST)
        {
            SoundManager.Instance.PlaySoundloop(past, player.transform);
        }
        else
        {
            SoundManager.Instance.PlaySoundloop(future, player.transform);
        }

    }

    private void moveToPrevPosition()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CharacterController>();
        trans = player.transform;

        // disable controller to allow teleporting

            controller.enabled = false;
            trans.position = GameManager.instance.playerPosition;
            trans.rotation = GameManager.instance.playerRotation;
   
            controller.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < transitionTime && playedAlready == false)
            {
                SoundManager.Instance.PlaySoundOnce(glitch, player.transform);
                transitionVolume.SetActive(true);
                playedAlready = true;
            }
        }
 
        else
        {
            remainingTime = 0;
            transitionVolume.SetActive(false);
            SceneManager.LoadScene(GameManager.instance.nextLevel(player.transform.position,player.transform.rotation));

        }

        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}

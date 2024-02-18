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

    private GameObject player;
    private CharacterController controller;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timeLimit;
        moveToPrevPosition();

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
            if (remainingTime < 6)
            {
                transitionVolume.SetActive(true);
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

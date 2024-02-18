using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour

{

    [SerializeField] TextMeshProUGUI timerText;
    float timeLimit = 5f;
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
        if (GameManager.instance.lastPlayerTransform != null)
        {
            controller.enabled = false;
            trans.position = GameManager.instance.lastPlayerTransform.position;
            trans.rotation = GameManager.instance.lastPlayerTransform.rotation;
            controller.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {  
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
            SceneManager.LoadScene(GameManager.instance.nextLevel(player.transform));
        }

        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}

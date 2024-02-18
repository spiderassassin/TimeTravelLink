// dave wrote this
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip jump;

    public AudioSource music;


    public AudioClip playerwalk;
    public AudioClip playerrun;
    public GameObject audioSource;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {


    }


    public GameObject PlaySoundloop(AudioClip clip, Transform parent)
    {
        GameObject obj = Instantiate(audioSource);
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        obj.GetComponent<AudioSource>().loop = true;
        obj.GetComponent<AudioSource>().clip = clip;
        obj.GetComponent<AudioSource>().Play();
        return obj;

    }

    public void PlaySoundOnce(AudioClip clip, Transform parent)
    {
        GameObject obj = Instantiate(audioSource);
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        obj.GetComponent<AudioSource>().loop = false;
        obj.GetComponent<AudioSource>().clip = clip;
        obj.GetComponent<AudioSource>().Play();
        StartCoroutine(Buffer(obj, clip.length));
    }

    public IEnumerator Buffer(GameObject obj, float len)
    {
        yield return new WaitForSeconds(len);
        StopSoundEffect(obj);


    }

    public void StopSoundEffect(GameObject obj)
    {
        Destroy(obj);
    }

    public void MusicChange()
    {

    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public event EventHandler<OnEggBreakEventArgs> OnEggBreak;
    public class OnEggBreakEventArgs : EventArgs { public int brokenEggId; }

    private Animator animator;
    [SerializeField] private GameObject wholeEggModel;
    [SerializeField] private GameObject brokenEggModel;

    [SerializeField] AudioClip eggCrackSound;
    [SerializeField] ParticleSystem eggBreakParticles;

    public enum EggState
    {
        WHOLE,
        BROKEN,
    }

    public int dinoEggId;
    private EggState state;

    public bool testBreakEgg = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        state = EggState.WHOLE;
    }

    public void BreakEgg()
    {
        if(state == EggState.WHOLE)
        {
            // Break the egg.
            SoundManager.Instance.PlaySoundOnce(eggCrackSound, transform);

            state = EggState.BROKEN;
            wholeEggModel.SetActive(false);
            brokenEggModel.SetActive(true);
            eggBreakParticles.Play();
            OnEggBreak?.Invoke(this, new OnEggBreakEventArgs { brokenEggId = dinoEggId }); ;
            
            // play egg crack sound
        }
    }

    private void Update()
    {
        if (testBreakEgg) { BreakEgg(); }
    }
}

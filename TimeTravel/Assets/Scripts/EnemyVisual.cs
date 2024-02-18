using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] Animator animator;

    public const string ANIM_STATE_IDLE = "Idle";
    public const string ANIM_STATE_BITE = "bite";
    public const string ANIM_STATE_RUN = "run";
    public const string ANIM_STATE_DANCING = "dancing";
    public const string ANIM_STATE_GANGNAM = "gangam";

    private int biteHash;
    private int runHash;
    private int danceHash;
    private int gangamHash;

    private Action onBiteEndAction;

    // Start is called before the first frame update
    void Start()
    {
        biteHash = Animator.StringToHash("bite");
        runHash = Animator.StringToHash("run");
        danceHash = Animator.StringToHash("dance");
        gangamHash = Animator.StringToHash("gangam");
    }

    public void PlayIdle()
    {
        animator.SetBool(runHash, false);
        animator.SetBool(biteHash, false);
        animator.SetBool(danceHash, false);
        animator.SetBool(gangamHash, false);
    }

    public void PlayBite(Action onBiteEndAction)
    {
        this.onBiteEndAction = onBiteEndAction;
        animator.Play(ANIM_STATE_BITE, -1, 0f);
        animator.SetBool(runHash, false);
        animator.SetBool(biteHash, true);
        animator.SetBool(danceHash, false);
        animator.SetBool(gangamHash, false);
    }

    public void EndBite()
    {
        onBiteEndAction.Invoke();
        animator.SetBool(biteHash, false);
    }

    public void PlayRun()
    {
        animator.SetBool(runHash, true);
        animator.SetBool(biteHash, false);
        animator.SetBool(danceHash, false);
        animator.SetBool(gangamHash, false);
    }

    public void PlayDance()
    {
        animator.SetBool(runHash, false);
        animator.SetBool(biteHash, false);
        animator.SetBool(danceHash, true);
        animator.SetBool(gangamHash, false);
    }

    public void PlayGangnam()
    {
        animator.SetBool(runHash, false);
        animator.SetBool(biteHash, false);
        animator.SetBool(danceHash, false);
        animator.SetBool(gangamHash, true);
    }
}

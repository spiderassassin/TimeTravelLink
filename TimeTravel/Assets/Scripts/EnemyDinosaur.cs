using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDinosaur : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    //[SerializeField] private Transform target;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyVisual enemyVisual;

    [SerializeField] private AudioClip roarSound;

    private enum EnemyState
    {
        Roaming,
        ChasePlayer,
        Attacking,
        GoingBackToStart,
    }

    private EnemyState state;

    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private float roamTimerCurrent;
    [SerializeField] private float roamTimerIntervalMin = 4f;
    [SerializeField] private float roamTimerIntervalMax = 8f;
    [SerializeField] private float roamDisplacementMin = 5f;
    [SerializeField] private float roamDisplacementMax = 40f;

    [SerializeField]  private float targetRange = 10f;
    [SerializeField]  private float stopChaseDistance = 30f;
    [SerializeField]  private float reachedPositionDistance = 5f;

    [SerializeField] private float attackRange = 0f;
    [SerializeField] private float attackRate = 1.2f;
    private float nextAttackTimer = 8f;

    private void Awake()
    {
        state = EnemyState.Roaming;
    }

    private void Start()
    {
        startingPosition = transform.position;
        SetNewRoamingPosition();
        navMeshAgent.destination = roamPosition;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Roaming:
                if (roamTimerCurrent > 0)
                {
                    roamTimerCurrent -= Time.deltaTime;
                }
                else
                {
                    SetNewRoamingPosition();
                    navMeshAgent.destination = roamPosition;
                }
                FindTarget();
                break;

            case EnemyState.ChasePlayer:
                navMeshAgent.destination = player.position;

                if (Vector3.Distance(transform.position, player.position) < attackRange)
                {
                    // Player is within attack range
                    if (Time.time > nextAttackTimer)
                    {

                        // ATTACK
                        Debug.Log("Dino attack player!");
                        enemyVisual.PlayBite(EndAttack);
                        // Deal damage to player
                        nextAttackTimer = Time.time + attackRate;
                        state = EnemyState.Attacking;

                        // (stop moving)
                        navMeshAgent.destination = transform.position;
                    }
                }
                else if (Vector3.Distance(transform.position, player.position) > stopChaseDistance)
                {
                    // Player is too far. Give up and stop chasing them.
                    state = EnemyState.GoingBackToStart;
                }
                break;
            case EnemyState.GoingBackToStart:
                navMeshAgent.destination = startingPosition;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                {
                    state = EnemyState.Roaming;
                }
                break;
            case EnemyState.Attacking:
                // Do nothing. Wait for the EndAttack() function to be triggered at the end of the bite animation.
                break;
        }

    }

    private void EndAttack()
    {
        if(state == EnemyState.Attacking)
        {
            Debug.Log("Dino attack animation end!");
            state = EnemyState.ChasePlayer;
        }
    }

    private void SetNewRoamingPosition()
    {
        roamPosition = GetRoamingPosition();
        roamTimerCurrent = Random.Range(roamTimerIntervalMin, roamTimerIntervalMax);
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f))
            .normalized;

        float randomDistance = Random.Range(roamDisplacementMin, roamDisplacementMax);

        return startingPosition + (randomDirection * randomDistance);
    }

    private void FindTarget()
    {
        if(Vector3.Distance(transform.position, player.position) < targetRange)
        {
            // Player is within target range
            state = EnemyState.ChasePlayer;
            SoundManager.Instance.PlaySoundOnce(roarSound, transform);
        }
    }

}

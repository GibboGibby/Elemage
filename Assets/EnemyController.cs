using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*
 * Enemy States - 
 *  - Idle (Each enemy will have a patrolling area, or they just stand still)
 *  - Chase (When they see a player they will chase their last known location)
 *  - Attack (If they reach the player they will attack)
 *  - LostTarget (If the target is lost they will patrol around their last known location for a bit before returning back to idle)
*/
public enum EnemyState
{
    Idle,
    Investigating,
    Chase,
    Attack,
    LostTarget
}

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemy Stats")]
    [SerializeField] private float health = 10f;
    [SerializeField] EnemyState currentState = EnemyState.Idle;
    [SerializeField] private float attackDistance = 2f;

    [SerializeField] private Transform[] patrolPath;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform test;
    private Rigidbody rb;
    private int pathValue = 0;
    private float alertMeter;
    [Header("Investigating Stats")]
    [SerializeField] private float alertPerSecond = 40f;
    [SerializeField] private float maxAlert = 105f;
    [SerializeField] private float timeForAlertDecay = 2f;
    [SerializeField] private float alertDecayAmount = 30f;

    private Vector3 targetLastKnown = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (patrolPath.Length == 0)
        {
            patrolPath = new Transform[1];
            patrolPath[0] = transform;
        }
        else
        {
            transform.position = patrolPath[0].position;
            transform.rotation = patrolPath[0].rotation;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        // Simple little state machine
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Investigating:
                Investigating();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.LostTarget:
                LostTarget();
                break;

        }
    }

    void Idle()
    {
        if (patrolPath.Length > 1)
        {

        }

        if (LookForTarget())
        {
            currentState = EnemyState.Investigating;
            alertMeter = 5f;
        }
    }

    bool LookForTarget()
    {
        // Check to see if the target is in the field of view of the enemy
        // If they are return true and set the last known position
        return false;
    }

    private float investTimer = 0f;
    void Investigating()
    {
        if (targetLastKnown != Vector3.zero)
        {
            agent.SetDestination(targetLastKnown);
        }
        if (LookForTarget())
        {
            alertMeter += alertPerSecond * Time.deltaTime;
            if (alertMeter > maxAlert)
            {
                currentState = EnemyState.Chase;
            }
        }
        else
        {
            alertMeter -= alertDecayAmount * Time.deltaTime;
            currentState = EnemyState.Idle;
            targetLastKnown = Vector3.zero;
        }


    }

    private float chaseTimer = 0f;
    void Chase()
    {
        if (targetLastKnown != Vector3.zero)
        {
            agent.SetDestination(targetLastKnown);
        }

        if (Vector3.Distance(targetLastKnown, transform.position) < attackDistance)
        {
            agent.isStopped = true;
            currentState = EnemyState.Attack;
        }

        if (LookForTarget())
        {
            chaseTimer = 0f;
            alertMeter = maxAlert;
        }
        else
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > timeForAlertDecay)
            {
                alertMeter -= alertDecayAmount;
                if (alertMeter < 0)
                {
                    currentState = EnemyState.LostTarget;
                }
            }
        }
    }

    void Attack()
    {
        // Deal Damage to Player
    }
    void LostTarget()
    {

    }

    public void EnemyHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EnemySleep()
    {
        rb.freezeRotation = false;
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}

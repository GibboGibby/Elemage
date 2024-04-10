using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

/*
 * Enemy States - 
 *  - Idle (Each enemy will have a patrolling area, or they just stand still)
 *  - Chase (When they see a player they will chase their last known location)
 *  - Attack (If they reach the player they will attack)
 *  - LostTarget (If the target is lost they will patrol around their last known location for a bit before returning back to idle)
*/

// https://www.youtube.com/watch?v=NXXc9xXzqnk for seeing enemies through walls
public enum EnemyState
{
    Idle,
    Investigating,
    Chase,
    Attack,
    LostTarget,
    Dead
}

[System.Serializable]
struct MyTransform
{
    public MyTransform(Vector3 pos, Quaternion rot)
    {
        Position = pos;
        Rotation = rot;
    }
    [SerializeField]
    public Vector3 Position;
    [SerializeField]
    public Quaternion Rotation;
}

public class EnemyController : MonoBehaviour
{

    public static bool DarkVisionOn = false;
    // Start is called before the first frame update
    [Header("Enemy Stats")]
    [SerializeField] private float health = 10f;
    [SerializeField] EnemyState currentState = EnemyState.Idle;
    [SerializeField] private float attackDistance = 2f;

    
    
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform test;
    private Rigidbody rb;
    private int pathValue = 0;
    
    [Header("Investigating Stats")]
    [SerializeField] private float alertPerSecond = 40f;
    [SerializeField] private float maxAlert = 105f;
    [SerializeField] private float timeForAlertDecay = 2f;
    [SerializeField] private float alertDecayAmount = 30f;
    [Header("Attack Stats")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 25f;
    [SerializeField] private float totalAttackTime = 1.5f;
    [SerializeField] private float timeUntilSleepKicksInIfAlert = 2.5f;

    [Header("Misc")]
    [SerializeField] private float enemyFov = 60f;
    [SerializeField] private float maxViewDist = 50f;
    private float enemyFovRad;
    [SerializeField] private bool drawFov;
    [SerializeField] private bool drawMaxViewDist;
    [SerializeField] private Transform eyePos;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float lostTargetSpinTime = 1f;
    

    [Header("Gameplay Stuff")]
    [SerializeField] private float alertMeter;
    [SerializeField] private List<MyTransform> patrolPath = new List<MyTransform>();
    [SerializeField] private List<Transform> publicPatrolPath;
    private Transform playerTransform;

    [Header("Enemy Alerting")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float radius;

    [Header("Cone Stuff")]
    [SerializeField] private MeshRenderer coneMr;
    [SerializeField] private Material invis;
    [SerializeField] private Material visionCone;

    private Vector3 targetLastKnown = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (publicPatrolPath.Count == 0)
        {
            patrolPath.Add(new MyTransform(transform.position, transform.rotation));
        }
        else
        {
            patrolPath[0] = new MyTransform(publicPatrolPath[0].position, publicPatrolPath[0].rotation);
            for (int i = 1; i < publicPatrolPath.Count; i++)
            {
                patrolPath.Add(new MyTransform(publicPatrolPath[i].position, publicPatrolPath[i].rotation));
            }
            transform.position = patrolPath[0].Position;
            transform.rotation = patrolPath[0].Rotation;
        }

        playerTransform = GameManager.Instance.GetPlayer().transform;
    }

    private void DrawFov()
    {
        Vector3 left = Quaternion.AngleAxis(-enemyFov / 2, Vector3.up) * transform.forward;
        Vector3 right = Quaternion.AngleAxis(enemyFov / 2, Vector3.up) * transform.forward;
        Debug.DrawLine(eyePos.position, eyePos.position + left * 10f, Color.red);
        Debug.DrawLine(eyePos.position, eyePos.position + right * 10f, Color.red);
    }

    private void DrawViewDist()
    {
        Debug.DrawLine(eyePos.position, eyePos.position + transform.forward * maxViewDist, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.DarkVisionOn)
        {
            coneMr.material = visionCone;
        }
        else
        {
            coneMr.material = invis;
        }

        if (drawFov)
            DrawFov();

        if (drawMaxViewDist)
            DrawViewDist();
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
            case EnemyState.Dead:
                return;
        }
    }

    bool idleResetDone = false;
    void Idle()
    {
        bool targetFound = LookForTarget();
        //Debug.Log("On idle func");
        if (patrolPath.Count > 1 && !targetFound)
        {
            agent.destination = patrolPath[pathValue].Position;
            if (Mathf.Abs(Vector3.Distance(transform.position, patrolPath[pathValue].Position)) < 0.3f)
            {
                pathValue++;
                if (pathValue > patrolPath.Count-1)
                {
                    pathValue = 0;
                }
            }
        }
        
        if (patrolPath.Count == 1 && !targetFound)
        {
            float dist = Vector3.Distance(transform.position, patrolPath[0].Position);
            if (transform.position != patrolPath[0].Position)
            {
                //Debug.Log("setting destination");
                agent.isStopped = false;
                agent.SetDestination(patrolPath[0].Position);
            }
            if (dist < 0.5f && !idleResetDone)
            {
                /*
                Debug.Log("This is actually called");
                agent.isStopped = true;
                transform.position = Vector3.Lerp(transform.position, patrolPath[0].Position, lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, patrolPath[0].Rotation, lerpSpeed * Time.deltaTime);
                if (dist < 0.01f)
                {
                    transform.position = patrolPath[0].Position;
                    transform.rotation = patrolPath[0].Rotation;
                }
                */
                idleResetDone = true;
                transform.position = patrolPath[0].Position;
                transform.rotation = patrolPath[0].Rotation;
            }
        }

        if (targetFound)
        {
            currentState = EnemyState.Investigating;
            idleResetDone = false;
            alertMeter = 5f;
        }
    }

    bool LookForTarget()
    {
        // Check to see if the target is in the field of view of the enemy
        // If they are return true and set the last known position
        if (playerTransform == null) playerTransform = GameManager.Instance.GetPlayer().transform;
        if (Vector3.Distance(transform.position, playerTransform.position) > maxViewDist) return false;

        Vector3 enemyToPlayer = (playerTransform.position - transform.position);
        enemyToPlayer.Normalize();
        float angle = Mathf.Acos(Vector3.Dot(enemyToPlayer, transform.forward) / enemyToPlayer.magnitude * transform.forward.magnitude);
        angle = Mathf.Rad2Deg * angle;
        if (angle < enemyFov / 2f)
        {
            //Debug.Log("Player in view");
            RaycastHit hit;
            if (Physics.Raycast(eyePos.position, enemyToPlayer, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    targetLastKnown = playerTransform.position;
                    return true;
                }
            }
        }
        return false;
    }

    private float investTimer = 0f;
    void Investigating()
    {
        //Debug.Log("on invest func");
        
        if (targetLastKnown != Vector3.zero)
        {
            agent.SetDestination(targetLastKnown);
        }
        if (LookForTarget())
        {
            alertMeter += alertPerSecond * Time.deltaTime;
            Mathf.Clamp(alertMeter, 0, maxAlert);
            if (alertMeter > maxAlert)
            {
                currentState = EnemyState.Chase;
            }
        }
        else
        {
            alertMeter -= alertDecayAmount * Time.deltaTime;
            if (alertMeter <= 0)
            {
                currentState = EnemyState.Idle;
                targetLastKnown = Vector3.zero;
            }
        }

    }

    public void GetAlerted(Vector3 playerPos)
    {
        targetLastKnown = playerPos;
        if (currentState != EnemyState.Chase && currentState != EnemyState.Attack) currentState = EnemyState.Chase;
        currentState = EnemyState.Chase;
        alertMeter = maxAlert;
        agent.SetDestination(playerPos);

    }

    private float chaseTimer = 0f;
    private bool noSpinReset = false;
    private bool spinDirRight = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector3 left = Quaternion.AngleAxis(-enemyFov / 2, Vector3.up) * transform.forward;
        Vector3 right = Quaternion.AngleAxis(enemyFov / 2, Vector3.up) * transform.forward;
        Gizmos.DrawLine(eyePos.position, eyePos.position + left * 10f);
        Gizmos.DrawLine(eyePos.position, eyePos.position + right * 10f);
    }
    void Chase()
    {
        //Debug.Log("On chase func");
        
        bool lookforTarget = LookForTarget();
        if (Vector3.Distance(playerTransform.position, transform.position) < attackDistance && lookforTarget)
        {
            agent.isStopped = true;
            currentState = EnemyState.Attack;
        }
        if (targetLastKnown != Vector3.zero)
        {
            agent.isStopped = false;
            agent.SetDestination(targetLastKnown);
        }

        if (lookforTarget)
        {
            chaseTimer = 0f;
            alertMeter = maxAlert;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetInstanceID() == gameObject.GetInstanceID()) continue;
                if (colliders[i].CompareTag("Enemy"))
                {
                    Debug.Log("Enemy found in the area");
                    colliders[i].GetComponent<EnemyController>().GetAlerted(targetLastKnown);
                }
            }
        }
        else
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > timeForAlertDecay)
            {
                alertMeter -= alertDecayAmount * Time.deltaTime;
                // Start Spinning
                if (!noSpinReset)
                {
                    spinDirRight = Random.value > 0.5f ? true : false;
                    noSpinReset = true;
                    agent.isStopped = true;
                    //agent.ResetPath();
                }

                float val = 360 / (maxAlert / alertDecayAmount);
                val = spinDirRight ? val * 1 : val * -1;
                //transform.Rotate(new Vector3(0f, val * Time.deltaTime, 0f), Space.Self);

                if (alertMeter < 0)
                {
                    currentState = EnemyState.LostTarget;
                    chaseTimer = 0f;
                    noSpinReset = false;
                    agent.isStopped = false;
                }
            }
        }
    }
    private float attackTimer = 0f;
    void Attack()
    {
        Debug.Log("On attack func");
        // Deal Damage to Player
        if (attackTimer == 0f)
            GameManager.Instance.GetPlayer().PlayerHit(attackDamage);
        attackTimer += Time.deltaTime;
        if (attackTimer >= totalAttackTime)
        {
            currentState = EnemyState.Chase;
            attackTimer = 0f;
        }
        // Instead of dealing instant damage do a swing with a collider and check if it hits the player
        // That way for ranged can do a bullet (maybe if the player is a certain range away they shoot)
    }

    private float lostTargetgSpinTimer = 0f;
    bool lostTargetSpinReset = false;
    bool lostSpinDir = false;
    void LostTarget()
    {
        //Debug.Log("On Target Lost Func");
        if (LookForTarget())
        {
            currentState = EnemyState.Investigating;
        }
        targetLastKnown = Vector3.zero;
        agent.isStopped = true;
        lostTargetgSpinTimer += Time.deltaTime;
        if (lostTargetgSpinTimer <= lostTargetSpinTime)
        {
            if (!lostTargetSpinReset)
            {
                lostSpinDir = Random.value > 0.5f ? true : false;
                lostTargetSpinReset = true;
                agent.isStopped = true;
            }

            float val = 360f / lostTargetSpinTime;
            val = spinDirRight ? val * 1 : val * -1;
            transform.Rotate(new Vector3(0f, val * Time.deltaTime, 0f), Space.Self);
        }
        else
        {
            agent.SetDestination(patrolPath[0].Position);
            currentState = EnemyState.Idle;
            lostTargetgSpinTimer = 0f;
            lostTargetSpinReset = false;
        }
        
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
        if (alertMeter > 50f)
            StartCoroutine(DelayedEnemySleep(timeUntilSleepKicksInIfAlert));
        else
            Sleep();
    }

    private void Sleep()
    {
        rb.freezeRotation = false;
        //this.enabled = false;
        //GetComponent<Collider>().enabled = false;
        agent.isStopped = true;
        agent.enabled = false;

        //transform.position += new Vector3(0f, 1.25f, 0f);

        rb.isKinematic = false;
        Vector3 temp = new Vector3(1f, 0, 1f);
        temp *= 20f;
        int bool1 = Random.Range(0, 2);
        int bool2 = Random.Range(0, 2);
        if (bool1 == 1) temp.x *= -1f;
        if (bool2 == 1) temp.z *= -1f;
        rb.AddForceAtPosition(temp, eyePos.position, ForceMode.Force);
        GetComponent<EnemyController>().enabled = false;
    }

    private IEnumerator DelayedEnemySleep(float time)
    {
        yield return new WaitForSeconds(time);
        Sleep();
    }
}

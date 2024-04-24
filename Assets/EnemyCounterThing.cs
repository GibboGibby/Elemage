using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounterThing : MonoBehaviour
{
    // Start is called before the first frame update

    public static EnemyCounterThing Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private int killed = 0;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CountEnemiesLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length;
    }

    public void IncrementKilledCounter()
    {
        killed++;
    }

    public int GetKilledCount()
    {
        return killed;
    }

    public void ResetCounters()
    {
        killed = 0;
    }
}

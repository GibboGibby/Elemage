using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GibDictEnemyController
{
    [SerializeField]
    GibDictEnemyControllerItem[] dictItems;

    public Dictionary<int, EnemyController> ToDictionary()
    {
        Dictionary<int, EnemyController> newDict = new Dictionary<int, EnemyController>();

        foreach (var item in dictItems)
        {
            newDict.Add(item.name, item.obj);
        }

        return newDict;
    }
}


[Serializable]
public class GibDictEnemyControllerItem
{
    [SerializeField]
    public int name;
    [SerializeField]
    public EnemyController obj;
}

public struct DominoEffector
{
    public List<EnemyController> enemies;
    public bool dominoed;
}



public class SpellEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpellEffectController Instance { get; private set; }

    private Dictionary<int, EnemyController> linkedEnemiesDict;
    [SerializeField] private GibDictEnemyController gibDict;

    public DominoEffector NewDominoEffector()
    {
        DominoEffector effector = new DominoEffector();
        effector.dominoed = false;
        effector.enemies = new List<EnemyController>();
        return effector;
    }

    private Dictionary<EnemyController, DominoEffector> dominoDict = new Dictionary<EnemyController, DominoEffector>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        linkedEnemiesDict = gibDict.ToDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfEnemyLinked(EnemyController ec)
    {
        //if (linkedEnemiesDict.ContainsKey(obj.GetInstanceID())) return true;
        if (dominoDict.ContainsKey(ec) && !dominoDict[ec].dominoed) return true;
        return false;
        //return false;
    }

    public void AddEnemyToLinked(EnemyController enemyOne, EnemyController enemyTwo)
    {
        //linkedEnemiesDict.Add(objID, enemyController);
        if(dominoDict.ContainsKey(enemyOne))
        {
            DominoEffector de = NewDominoEffector();
            de.enemies.Add(enemyTwo);
            dominoDict[enemyOne] = de;
        }
        else
        {
            dominoDict[enemyOne].enemies.Add(enemyTwo);
        }
    }
}

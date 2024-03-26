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

public class SpellEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpellEffectController Instance { get; private set; }

    private Dictionary<int, EnemyController> linkedEnemiesDict;
    [SerializeField] private GibDictEnemyController gibDict;

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

    public bool CheckIfEnemyLinked(GameObject obj)
    {
        if (linkedEnemiesDict.ContainsKey(obj.GetInstanceID())) return true;
        return false;
    }

    public void AddEnemyToLinked(int objID, EnemyController enemyController)
    {
        linkedEnemiesDict.Add(objID, enemyController);
    }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[Serializable]
public class GibDict
{
    [SerializeField]
    GibDictItem[] dictItems;

    public Dictionary<string, GameObject> ToDictionary()
    {
        Dictionary<string, GameObject> newDict = new Dictionary<string, GameObject>();

        foreach (var item in dictItems)
        {
            newDict.Add(item.name, item.obj);
        }

        return newDict;
    }
}


[Serializable]
public class GibDictItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public GameObject obj;
}

public class ProjectileSupplier : MonoBehaviour
{

    public Dictionary<string, GameObject> prefabs;
    [SerializeField] private GibDict gibDict;
    public static ProjectileSupplier Instance { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        prefabs = gibDict.ToDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

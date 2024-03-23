using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : ProjectileBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHitEnemy()
    {
        Debug.Log("Specifically fireball has hit an enemy");
    }

    public override void OnHitNothing()
    {
        Debug.Log("Specifically fireball has hit nothing");
    }
}

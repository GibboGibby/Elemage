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

    public override void OnHitEnemy(GameObject enemy)
    {
        Debug.Log("Specifically fireball has hit an enemy");
        enemy.GetComponent<EnemyController>().EnemyHit(20f);
        Destroy(gameObject);
    }

    public override void OnHitNothing(GameObject enemy)
    {
        Debug.Log("Specifically fireball has hit nothing");
        Destroy(gameObject);
    }
}

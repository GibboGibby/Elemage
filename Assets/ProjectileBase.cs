using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnHitEnemy(GameObject enemy)
    {
        Debug.Log("projectile has hit enemy");
    }

    public virtual void OnHitNothing(GameObject enemy)
    {
        Debug.Log("projectile has hit nothing");
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            OnHitEnemy(collision.gameObject);
        }
        else
        {
            
            OnHitNothing(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningOrb : SpellMono
{

    private RaycastBase raycaster = new RaycastBase();
    private float range = 10f;
    private float mainDamage = 5f;
    private float falloffDamage = 3f;
    public override void OnHold()
    {
        
    }

    public override void OnPress()
    {
        
    }

    public override void OnRelease(bool isRightHand)
    {
        //RaycastHit hit;
        if (RaycastBase.AbilityRaycast("Enemy", 20f, out RaycastHit hit))
        {
            Debug.Log("Enemy found and firing lightning bolt at");
            Debug.Log(hit.collider.gameObject.name);
            
            hit.collider.gameObject.GetComponent<EnemyController>().EnemyHit(mainDamage);

            Collider[] colliders;
            colliders = Physics.OverlapSphere(hit.transform.position, range);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy") && collider.gameObject != hit.collider.gameObject)
                {
                    collider.gameObject.GetComponent<EnemyController>().EnemyHit(falloffDamage);
                }
            }
        }

        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }
}

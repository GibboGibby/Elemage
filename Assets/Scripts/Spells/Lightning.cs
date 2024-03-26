using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lightning : SpellMono
{

    private RaycastBase raycaster = new RaycastBase();
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
            
            hit.collider.gameObject.GetComponent<EnemyController>().EnemyHit(5f);
        }

        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningBolt : SpellMono
{
    // https://www.youtube.com/watch?v=NMTXHnsxgss Lightning Effect
    // https://www.patreon.com/posts/vfx-breakdown-71191108 Text Instructions ^^
    private RaycastBase raycaster = new RaycastBase();
    private float range = 20f;
    private float mainDamage = 5f;
    private float falloffDamage = 2f;
    private int maxBounces = 3;

    public LightningBolt()
    {
        spellName = "Lightning Bolt";
    }
    public override void OnHold()
    {
        
    }

    public override void OnPress()
    {
        
    }

    public override void OnRelease(bool isRightHand)
    {
        //RaycastHit hit;
        bool rayHit = false;
        
        if (RaycastBase.AbilityRaycast("EnemyTwo", 20f, out RaycastHit hit))
        {
            PlaySound("lightning_bolt");
            rayHit = true;
            Debug.Log("Enemy found and firing lightning bolt at");
            Debug.Log(hit.collider.gameObject.name);
            
            hit.collider.gameObject.GetComponent<EnemyController>().EnemyHit(mainDamage);

            List<GameObject> objectsHit = new List<GameObject>();
            objectsHit.Add(hit.collider.gameObject);
            GameObject currentTarget = hit.collider.gameObject;
            for (int i = 0; i < maxBounces; i++)
            {
                Collider[] colliders;
                colliders = Physics.OverlapSphere(currentTarget.transform.position, range);
                if (colliders == null) break;
                GameObject temp = colliders[0].gameObject;
                float maxDist = 9999f;
                bool foundAnyEnemies = false;
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy") && collider.gameObject != currentTarget && !objectsHit.Contains(collider.gameObject))
                    {
                        foundAnyEnemies = true;
                        //collider.gameObject.GetComponent<EnemyController>().EnemyHit(falloffDamage);
                        float curDist = Vector3.Distance(currentTarget.transform.position, collider.gameObject.transform.position);
                        if (curDist < maxDist)
                        {
                            maxDist = curDist;
                            temp = collider.gameObject;
                        }
                    }
                }
                if (!foundAnyEnemies) continue;
                temp.gameObject.GetComponent<EnemyController>().EnemyHit(falloffDamage);
                objectsHit.Add(temp);
                Debug.DrawLine(currentTarget.transform.position, temp.transform.position, Color.red, 100f);
                currentTarget = temp;

            }

            GameObject lightning = Instantiate(ProjectileSupplier.Instance.prefabs["lightning_bolt"]);
            LineRenderer lr = lightning.GetComponent<LineRenderer>();
            lr.positionCount = 1;
            lr.SetPosition(0, transform.position);
            for (int i = 0; i < objectsHit.Count; i++)
            {
                lr.positionCount++;
                lr.SetPosition(i + 1, objectsHit[i].transform.position);
            }
            StartCoroutine(LightningBoltStuff(isRightHand, lightning));
        }

        if (!rayHit)
        {
            PlaySound("fizzle");
            GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
        }
    }

    private IEnumerator LightningBoltStuff(bool rightHand, GameObject lightningBolt)
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerSpellController>().RemoveSpellFromHand(rightHand);
        Destroy(lightningBolt);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LightningOrb : SpellMono
{
    // https://www.youtube.com/watch?v=NMTXHnsxgss Lightning Effect
    // https://www.patreon.com/posts/vfx-breakdown-71191108 Text Instructions ^^

    private RaycastBase raycaster = new RaycastBase();
    private float range = 5f; // Radius
    private float mainDamage = 5f;
    private float falloffDamage = 3f;

    public LightningOrb()
    {
        spellName = "Lightning Orb";
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
            rayHit = true;
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

            GameObject orb = Instantiate(ProjectileSupplier.Instance.prefabs["lightning_orb"]);
            orb.transform.position = hit.transform.position;
            LineRenderer lr = orb.GetComponent<LineRenderer>();
            MeshRenderer mr = orb.GetComponent<MeshRenderer>();
            mr.enabled = false;
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.collider.transform.position);
            StartCoroutine(LightningOrbStuff(isRightHand, orb, lr, mr));
        }
        if (!rayHit)
        {
            GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
            PlaySound("fizzle");
        }
    }

    private IEnumerator LightningOrbStuff(bool isRightHand, GameObject obj, LineRenderer lr, MeshRenderer mr)
    {
        yield return new WaitForSeconds(0.2f);
        lr.enabled = false;
        float scale = 0.1f;
        obj.transform.localScale = new Vector3(scale, scale, scale);
        mr.enabled = true;
        float elapsed = 0.0f;

        float maxTime = 0.1f;
        float maxScale = range * 2;
        PlaySound("lightning_orb");
        while (elapsed <= maxTime)
        {
            elapsed += Time.deltaTime;
            float temp = elapsed / maxTime;
            float dif = maxScale - scale;
            float newScale = scale + dif * temp;
            obj.transform.localScale = new Vector3(newScale, newScale, newScale);
            //obj.transform.localScale += new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(obj);
        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
        
    }
    
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blink : SpellMono
{
    private Camera mainCam;

    private float maxDist = 30f;
    private IEnumerator BlinkCoroutine(Vector3 startPos, Vector3 endPos, bool isRightHand)
    {
        float elapsed = 0f;
        float totalTime = 0.125f;

        while (elapsed <  totalTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed * (1f / totalTime));
            elapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }

    Vector3 targetPos = Vector3.zero;

    public Blink()
    {
        spellName = "Blink";
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    public override void OnHold()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, maxDist))
        {
            Debug.Log("Blink ray hit something");
            targetPos = hit.point;
        }
        else
        {
            targetPos = transform.position + mainCam.transform.forward * maxDist; 
        }
    }

    public override void OnPress()
    {
        
    }

    public override void OnRelease(bool isRightHand)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //StartCoroutine(BlinkCoroutine(transform.position, transform.position + mainCam.transform.forward * 50f,isRightHand));
        PlaySound("blink");
        StartCoroutine(BlinkCoroutine(transform.position, targetPos,isRightHand));
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blink : SpellMono
{
    private Camera mainCam;
    private IEnumerator BlinkCoroutine(Vector3 startPos, Vector3 endPos, bool isRightHand)
    {
        float elapsed = 0f;
        float totalTime = 0.2f;

        while (elapsed <  totalTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed * 5);
            elapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }

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
        
    }

    public override void OnPress()
    {
        
    }

    public override void OnRelease(bool isRightHand)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(BlinkCoroutine(transform.position, transform.position + mainCam.transform.forward * 50f,isRightHand));
    }
}

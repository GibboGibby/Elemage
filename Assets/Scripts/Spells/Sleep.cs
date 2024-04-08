using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : SpellMono
{
    // Start is called before the first frame update

    Camera mainCam;

    public Sleep()
    {
        spellName = "Sleep";
    }

    private void Start()
    {
        mainCam = Camera.main;
    }
    public override void OnHold()
    {
        Debug.Log("Slept Holding");
    }

    public override void OnPress()
    {
        Debug.Log("Slept Press");
    }

    public override void OnRelease(bool isRightHand)
    {
        Debug.Log("Slept Release");

        GameObject fireball = Instantiate(ProjectileSupplier.Instance.prefabs["sleep"]);
        fireball.transform.position = mainCam.transform.position + mainCam.transform.forward;
        fireball.GetComponent<Rigidbody>().AddForce(mainCam.transform.forward * 20f, ForceMode.Impulse);
        fireball.transform.rotation = mainCam.transform.rotation;

        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }
}

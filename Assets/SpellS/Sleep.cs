using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : SpellMono
{
    // Start is called before the first frame update

    public Sleep()
    {
        spellName = "Sleep";
    }
    public override void OnHold()
    {
        Debug.Log("Slept Holding");
    }

    public override void OnPress()
    {
        Debug.Log("Slept Press");
    }

    public override void OnRelease()
    {
        Debug.Log("Slept Release");
    }
}

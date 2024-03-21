using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SpellMono
{
    // Start is called before the first frame update
    
    public Fireball()
    {
        spellName = "Fireball";
    }

    public override void OnHold()
    {
        Debug.Log("Aiming Fireball");
        //throw new System.NotImplementedException();
    }

    public override void OnPress()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnRelease()
    {
        Debug.Log("Fireball fired");
        //throw new System.NotImplementedException();
    }
}

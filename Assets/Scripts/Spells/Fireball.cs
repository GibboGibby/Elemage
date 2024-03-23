using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SpellMono
{
    // Start is called before the first frame update
    private Camera main;

    public Fireball()
    {
        spellName = "Fireball";
    }

    private void Start()
    {
        main = Camera.main;
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

    public override void OnRelease(bool isRightHand)
    {
        Debug.Log("Fireball fired");
        //throw new System.NotImplementedException();

        GameObject fireball = Instantiate(ProjectileSupplier.Instance.prefabs["fireball"]);
        fireball.transform.position = main.transform.position + main.transform.forward;
        fireball.GetComponent<Rigidbody>().AddForce(main.transform.forward * 50f, ForceMode.Impulse);

        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }
}

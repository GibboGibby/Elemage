using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizzle : SpellMono
{
    public override void OnHold()
    {
        
    }

    public override void OnPress()
    {
        
    }

    public override void OnRelease(bool isRightHand)
    {
        Debug.Log("Fizzle");
        GameManager.Instance.GetSpellAudio().PlaySound("fizzle");
        GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
    }
}

public class IceBlock : Fizzle
{
    public IceBlock() { spellName = "Ice Trap"; }
}

public class BoxTrap : Fizzle
{
    public BoxTrap() { spellName = "Box Trap"; }
}

public class Silence : Fizzle
{
    public Silence() { spellName = "Silence"; }
}

public class Tornado : Fizzle
{
    public Tornado() { spellName = "Tornado"; }
}

public class SummonMinion : Fizzle
{
    public SummonMinion() { spellName = "Summon Minion"; }
}
public class AlterMind : Fizzle
{
    public AlterMind() { spellName = "Alter Mind"; }
}

public class Abyss : Fizzle
{
   public Abyss() { spellName = "Abyss"; }
}

public class Domino : Fizzle
{
    public Domino() { spellName = "Domino"; }
}

public class Forcefield : Fizzle
{
    public Forcefield() { spellName = "Forcefield"; }
}
public class Telekinesis : Fizzle
{
    public Telekinesis() { spellName = "Telekinesis"; }
}

public class WallTrap : Fizzle
{
    public WallTrap() { spellName = "Wall Trap"; }
}
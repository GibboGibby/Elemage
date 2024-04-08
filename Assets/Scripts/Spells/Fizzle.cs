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
        
    }
}

public class IceBlock : Fizzle
{
    IceBlock() { spellName = "Ice Trap"; }
}

public class BoxTrap : Fizzle
{
    BoxTrap() { spellName = "Box Trap"; }
}

public class Silence : Fizzle
{
    Silence() { spellName = "Silence"; }
}
public class BendTime : Fizzle
{
    BendTime() { spellName = "Bend Time"; }
}
public class Tornado : Fizzle
{
    Tornado() { spellName = "Tornado"; }
}

public class SummonMinion : Fizzle
{
    SummonMinion() { spellName = "Summon Minion"; }
}
public class AlterMind : Fizzle
{
    AlterMind() { spellName = "Alter Mind"; }
}

public class Abyss : Fizzle
{
    Abyss() { spellName = "Abyss"; }
}

public class Domino : Fizzle
{
    Domino() { spellName = "Domino"; }
}

public class Forcefield : Fizzle
{
    Forcefield() { spellName = "Forcefield"; }
}
public class Telekinesis : Fizzle
{
    Telekinesis() { spellName = "Telekinesis"; }
}

public class WallTrap : Fizzle
{
    WallTrap() { spellName = "Wall Trap"; }
}
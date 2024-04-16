using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpellMono : MonoBehaviour
{
    public string spellName = "base spell mono";
    public abstract void OnPress();
    public abstract void OnHold();
    public abstract void OnRelease(bool isRightHand);

    protected void PlaySound(string spell)
    {
        GameManager.Instance.GetSpellAudio().PlaySound(spell);
    }
}

public class SpellManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Dictionary<string, SpellMono> SpellMonos = new Dictionary<string, SpellMono>() {
        {"fireball", new Fireball() },
        {"sleep", new Sleep() },
        {"blink", new Blink() },
        {"lightning_orb", new LightningOrb() },
        {"lightning_bolt", new LightningBolt() },
        {"dark_vision", new DarkVision() },
        {"ice_block", new IceBlock() },
        {"box_trap", new BoxTrap() },
        {"silence", new Silence() },
        {"bend_time", new BendTime() },
        {"tornado", new Tornado() },
        {"summon_minion", new SummonMinion() },
        {"alter_mind", new AlterMind() },
        {"abyss", new Abyss() },
        {"domino", new Domino() },
        {"forcefield", new Forcefield() },
        {"telekinesis", new Telekinesis() },
        {"wall_trap", new WallTrap() },
    };


    public static Dictionary<string, SpellShape> SpellShapes = new Dictionary<string, SpellShape>()
    {
        { "sleep", new SpellShape(new int[,]{
            { 1,2,3},
            {0, 4, 0},
            {5,6,7,}
        }, false)},

        { "fireball", new SpellShape(new int[,]{
            {0,4,3},
            {5,2,0},
            {0,1,0}
        }, true)},

        { "blink", new SpellShape(new int[,]{
            {0,5,0},
            {2,3,4},
            {1,0,0}
        }, true)},

        { "lightning_orb", new SpellShape(new int[,]{
            {9,8,6},
            {7,5,3},
            {4,2,1}
        }, false)},

        { "lightning_bolt", new SpellShape(new int[,]{
            {0,1,0},
            {2,3,4},
            {0,5,0}
        }, true)},
        
        { "dark_vision", new SpellShape(new int[,]{
            {0,1,0},
            {2,0,4},
            {0,3,0}
        }, true)},

        { "ice_block", new SpellShape(new int[,]{
            {0,7,6},
            {4,1,5},
            {3,2,0}
        }, true)},

        { "box_trap", new SpellShape(new int[,]{
            {1,8,7},
            {2,0,6},
            {3,4,5}
        }, true)},

        { "silence", new SpellShape(new int[,]{
            {3,2,1},
            {4,5,6},
            {9,8,7}
        }, false)},

        { "bend_time", new SpellShape(new int[,]{
            {5,6,7},
            {9,4,8},
            {1,2,3}
        }, false)},

        { "tornado", new SpellShape(new int[,]{
            {1,2,3},
            {8,9,4},
            {7,6,5}
        }, false)},

        { "summon_minion", new SpellShape(new int[,]{
            {3,0,5},
            {2,4,6},
            {1,0,7}
        }, true)},

        { "alter_mind", new SpellShape(new int[,]{
            {3,7,8},
            {2,4,6},
            {1,5,0}
        }, true)},

        { "abyss", new SpellShape(new int[,]{
            {1,0,6},
            {0,2,0},
            {5,4,3}
        }, true)},

        { "domino", new SpellShape(new int[,]{
            {0,2,0},
            {1,4,0},
            {5,0,3}
        }, true)},

        { "forcefield", new SpellShape(new int[,]{
            {0,3,0},
            {2,0,4},
            {1,6,5}
        }, true)},

        { "telekinesis", new SpellShape(new int[,]{
            {4,3,0},
            {5,2,0},
            {0,1,0}
        }, true)},

        { "wall_trap", new SpellShape(new int[,]{
            {0,3,4},
            {0,2,0},
            {5,1,0}
        }, true)},
    };

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

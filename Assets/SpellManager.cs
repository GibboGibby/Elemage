using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpellMono : MonoBehaviour
{
    public string spellName = "base spell mono";
    public abstract void OnPress();
    public abstract void OnHold();
    public abstract void OnRelease(bool isRightHand);
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
        {"dark_vision", new DarkVision() }
    };


    public static Dictionary<string, SpellShape> SpellShapes = new Dictionary<string, SpellShape>()
    {
        { "sleep", new SpellShape(new int[,]{
            { 1,2,3},
            {0, 4, 0},
            {5,6,7,}
        }, true)},

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
        }, true)},

        { "lightning_bolt", new SpellShape(new int[,]{
            {5,3,0},
            {4,2,0},
            {0,0,1}
        }, true)},
        
        { "dark_vision", new SpellShape(new int[,]{
            {0,1,0},
            {2,0,4},
            {0,3,0}
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

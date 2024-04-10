using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;

public class VoiceController : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private SpellShape spellShape;
    private SpellShape fireballShape;

    [SerializeField] private SpellInputController leftHandSpellInputController;
    [SerializeField] private SpellInputController rightHandSpellInputController;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private PlayerSpellController playerSpellController;

    public void StartKeywordRecognizer()
    {
        keywordRecognizer.Start();
    }

    public void StopKeywordRecognizer()
    {
        keywordRecognizer.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpellShape.Position[] shape = { SpellShape.Position.TopLeft, SpellShape.Position.TopMiddle, SpellShape.Position.TopRight,
        SpellShape.Position.MidMiddle,
        SpellShape.Position.BottomLeft, SpellShape.Position.BottomMiddle, SpellShape.Position.BottomRight };

        SpellShape.Position[] fireShape =
        {
            SpellShape.Position.BottomMiddle, SpellShape.Position.MidMiddle, SpellShape.Position.TopRight, SpellShape.Position.TopMiddle, SpellShape.Position.MidLeft
        };
        fireballShape = new SpellShape(fireShape, fireShape.Length);
        spellShape = new SpellShape(shape, shape.Length);

        spellShape.SetReversable(true);
        fireballShape.SetReversable(true);

        text.text = "";

        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("forward", Forward);
        actions.Add("front", Forward);
        actions.Add("azarath metrion zinthos", Back);
        actions.Add("back", Back);
        actions.Add("fireball", Fireball);
        actions.Add("sleep", Sleep);
        actions.Add("blink", Blink);
        actions.Add("lightning orb", Lightning);
        actions.Add("lightning bolt", LightningBolt);
        actions.Add("dark vision", DarkVision);
        actions.Add("ice block", IceBlock);
        actions.Add("box trap", BoxTrap);
        actions.Add("silence", Silence);
        actions.Add("bend time", BendTime);
        actions.Add("tornado", Tornado);
        actions.Add("summon minion", SummonMinion);
        actions.Add("alter mind", AlterMind);
        actions.Add("abyss", Abyss);
        actions.Add("domino", Domino);
        actions.Add("forcefield", Forcefield);
        actions.Add("telekinesis", Telekinesis);
        actions.Add("wall trap", WallTrap);
        //actions.Add("somnum", Sleep);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognisedSpeech;
        //keywordRecognizer.Start();
    }

    private void PrintOutSpell(SpellShape shape)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(shape.GetRowAsString(i));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintOutSpell(SpellManager.SpellShapes["sleep"]);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var key in actions.Keys)
            {
                actions[key].Invoke();
            }
        }
    }

    private void RecognisedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Forward()
    {
        transform.Translate(1,0, 0);
    }
    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }

    // 1 = left, 2 = right and 3 = both
    private void SpellFailed(int id)
    {
        if (id == 1) // Left
        {
            leftHandSpellInputController.transform.parent.GetComponent<SpellCanvasController>().Shake();
        }
        else if (id == 2) // Right
        {
            rightHandSpellInputController.transform.parent.GetComponent<SpellCanvasController>().Shake();
        }
    }

    private void AddSpellToHand(string spell)
    {
        if (!SpellManager.SpellShapes.ContainsKey(spell) || !SpellManager.SpellMonos.ContainsKey(spell))
        {
            Debug.LogError("big error, cannot find spell");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            SpellShape temp = new SpellShape(leftHandSpellInputController.GetCurrentOrder().ToArray(), leftHandSpellInputController.GetCurrentOrder().Count);
            SpellShape other = SpellManager.SpellShapes[spell];
            Debug.Log("Other: ");
            PrintOutSpell(other);
            if (temp == other)
            {
                text.text = spell + " has been cast";
                Debug.Log(spell + " on left hand");

                playerSpellController.AddSpellToHand(SpellManager.SpellMonos[spell], false);
            }
            else if (temp != other) { SpellFailed(1); }
        }

        if (Input.GetKey(KeyCode.E))
        {
            SpellShape temp = new SpellShape(rightHandSpellInputController.GetCurrentOrder().ToArray(), rightHandSpellInputController.GetCurrentOrder().Count);
            SpellShape other = SpellManager.SpellShapes[spell];
            Debug.Log("Other: ");
            PrintOutSpell(other);
            if (temp == other)
            {
                text.text = spell + " has been cast";
                Debug.Log(spell + " on right hand");

                playerSpellController.AddSpellToHand(SpellManager.SpellMonos[spell], true);
            }
            else SpellFailed(2);
        }

        if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            Debug.Log("It is always here");
            SpellShape leftTemp = new SpellShape(leftHandSpellInputController.GetCurrentOrder().ToArray(), leftHandSpellInputController.GetCurrentOrder().Count);
            if (leftTemp == SpellManager.SpellShapes[spell])
            {
                text.text = spell + " has been cast";
                Debug.Log(spell + " on left hand");

                playerSpellController.AddSpellToHand(SpellManager.SpellMonos[spell], false);
            }
            else SpellFailed(1);
            SpellShape temp = new SpellShape(rightHandSpellInputController.GetCurrentOrder().ToArray(), rightHandSpellInputController.GetCurrentOrder().Count);
            if (temp == SpellManager.SpellShapes[spell])
            {
                text.text = spell + " has been cast";
                Debug.Log(spell + " on left hand");

                playerSpellController.AddSpellToHand(SpellManager.SpellMonos[spell], true);
            }
            else SpellFailed(2);

        }
    }

    private void Fireball()
    {
        AddSpellToHand("fireball");
    }

    private void Sleep()
    {
        Debug.Log("This sleep is being called");
        AddSpellToHand("sleep");
    }

    private void Lightning()
    {
        Debug.Log("Lightning is being called");
        AddSpellToHand("lightning_orb");
    }

    private void LightningBolt()
    {
        AddSpellToHand("lightning_bolt");
    }

    private void Blink()
    {
        AddSpellToHand("blink");
    }

    private void DarkVision()
    {
        AddSpellToHand("dark_vision");
    }

    private void IceBlock() { AddSpellToHand("ice_block"); }
    private void BoxTrap() { AddSpellToHand("box_trap"); }
    private void Silence() { AddSpellToHand("silence"); }
    private void BendTime() { AddSpellToHand("bend_time"); }
    private void Tornado() { AddSpellToHand("tornado"); }
    private void SummonMinion() { AddSpellToHand("summon_minion"); }
    private void AlterMind() { AddSpellToHand("alter_mind"); }
    private void Abyss() { AddSpellToHand("abyss"); }
    private void Domino() { AddSpellToHand("domino"); }
    private void Forcefield() { AddSpellToHand("forcefield"); }
    private void Telekinesis() { AddSpellToHand("telekinesis"); }
    private void WallTrap() { AddSpellToHand("wall_trap"); }

}

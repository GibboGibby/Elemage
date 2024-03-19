using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;

public class VoiceThing : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private SpellShape spellShape;
    private SpellShape fireballShape;

    [SerializeField] private SpellInputController spellController;
    [SerializeField] private TextMeshProUGUI text;

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
        actions.Add("somnum", Sleep);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognisedSpeech;
        keywordRecognizer.Start();
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
            PrintOutSpell(spellShape);
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

    private void Fireball()
    {
        Debug.Log("Fireball has been cast");
        SpellShape temp = new SpellShape(spellController.GetCurrentOrder().ToArray(), spellController.GetCurrentOrder().Count);
        if (temp == fireballShape)
        {
            text.text = "Fireball has been cast";
        }
    }

    private void Sleep()
    {
        Debug.Log("Sleep recognised");
        SpellShape temp = new SpellShape(spellController.GetCurrentOrder().ToArray(), spellController.GetCurrentOrder().Count);
        if (temp == spellShape)
        {
            Debug.Log("Sleep successfully cast");
            text.text = "Sleep has been cast";
        }
    }

}

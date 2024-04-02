using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpellUpdater : MonoBehaviour
{
    [SerializeField] private PlayerSpellController playerSpells;
    [SerializeField] private TextMeshProUGUI leftMain;
    [SerializeField] private TextMeshProUGUI leftSide;
    [SerializeField] private TextMeshProUGUI rightMain;
    [SerializeField] private TextMeshProUGUI rightSide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] spells = playerSpells.GetSpells();
        leftMain.text = spells[0];
        leftSide.text = spells[1];
        rightMain.text = spells[2];
        rightSide.text = spells[3];
    }
}

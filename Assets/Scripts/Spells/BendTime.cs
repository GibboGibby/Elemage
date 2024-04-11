using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTime : SpellMono
{
   public BendTime()
    {
        spellName = "Bend Time";
    }

    static bool BendTimeActive = false;

    private float maxTime = 2f;

    public override void OnHold()
    {
        
    }

    public override void OnPress()
    {
        
    }
    private bool started = false;
    private float timer = 0f;

    bool isRightHandLocal = false;
    private void Update()
    {
        if (started && !PauseMenuController.IsPaused)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                Debug.Log("EndVision");
                EndBendTime();
                GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHandLocal);
            }
        }
    }

    public void StartBendTime()
    {
        Time.timeScale = 0.2f;
        BendTimeActive = true;
        GetComponent<PlayerController>().ChangeSpeed(5f);
        
    }

    public void EndBendTime()
    {
        BendTimeActive = false;
        Time.timeScale = 1f;
        GetComponent<PlayerController>().ChangeSpeed(0.2f);
    }

    public override void OnRelease(bool isRightHand)
    {
        isRightHandLocal = isRightHand;
        if (!started)
        {
            StartBendTime();
        }
        else
        {
            EndBendTime();
            GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
        }
        started = true;
    }
}

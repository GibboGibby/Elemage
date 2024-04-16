using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DarkVision : SpellMono
{
    // Start is called before the first frame update
    private float maxTime = 5f;

    public DarkVision()
    {
        spellName = "Dark Vision";
    }

    bool started = false;
    bool isRightHandLocal;

    public override void OnHold()
    {
        
    }

    public override void OnPress()
    {
        
    }

    private float timer = 0f;
    private void Update()
    {

        if (started && !PauseMenuController.IsPaused)
        {
            timer += Time.deltaTime;
            StartDarkVision();
            if (timer >= maxTime)
            {
                Debug.Log("EndVision");
                EndDarkVision();
                GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHandLocal);
                PlaySound("dark_vision_end");
            }
        }
    }

    private void EndDarkVision()
    {
        //UniversalRenderPipelineUtils.SetRendererFeatureActive("DarkVisionPartOne", false);
        //UniversalRenderPipelineUtils.SetRendererFeatureActive("DarkVisionPartTwo", false);
        GameManager.Instance.SetDarkVision(false);
        EnemyController.DarkVisionOn = false;
    }

    private void StartDarkVision()
    {
        //UniversalRenderPipelineUtils.SetRendererFeatureActive("DarkVisionPartOne", true);
        //UniversalRenderPipelineUtils.SetRendererFeatureActive("DarkVisionPartTwo", true);
        GameManager.Instance.SetDarkVision(true);
        EnemyController.DarkVisionOn = true;
    }

    public override void OnRelease(bool isRightHand)
    {
        isRightHandLocal = isRightHand;
        if (!started)
        {
            StartDarkVision();
            PlaySound("dark_vision");
        }
        else
        {
            EndDarkVision();
            GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
            PlaySound("dark_vision_end");
        }
        started = true;
    }
}

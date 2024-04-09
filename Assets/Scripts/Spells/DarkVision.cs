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
        spellName = "DarkVision";
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

        if (started)
        {
            timer += Time.deltaTime;
            StartDarkVision();
            if (timer >= maxTime)
            {
                Debug.Log("EndVision");
                EndDarkVision();
                GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHandLocal);
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
        //isRightHandLocal = isRightHand;
        if (!started)
        {
            StartDarkVision();
        }
        else
        {
            EndDarkVision();
            GetComponent<PlayerSpellController>().RemoveSpellFromHand(isRightHand);
        }
        started = true;
    }
}

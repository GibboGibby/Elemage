using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGrowAndShrink : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;

    [SerializeField] private float lerpTime = 0.5f;
    [SerializeField] private bool up = true;
    void Start()
    {
        
    }
    public float EaseInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }
    // Update is called once per frame
    private float currentTime = 0f;
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lerpTime)
        {
            currentTime = 0f;
            up = !up;
        }
        float scale = 1f;
        if (up)
        {
            //transform.position = Vector3.Lerp(bottomPos, upPos, currentTime / lerpTime);
            //transform.position = Vector3.Lerp(bottomPos, upPos, EaseInOutQuad(0, 1, currentTime / lerpTime));
            scale = Mathf.Lerp(maxSize, minSize, EaseInOutQuad(0, 1, currentTime / lerpTime));
        }
        else
        {
            //transform.position = Vector3.Lerp(upPos, bottomPos, currentTime / lerpTime);
            //transform.position = Vector3.Lerp(upPos, bottomPos, EaseInOutQuad(0, 1, currentTime / lerpTime));
            scale = Mathf.Lerp(minSize, maxSize, EaseInOutQuad(0, 1, currentTime / lerpTime));
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform camTransform;
    [SerializeField] private float shakeDuration = 0.2f;
    private float tempShake;

    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;

    Vector3 origPos;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        origPos = transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tempShake > 0)
        {
            transform.localPosition = origPos + Random.insideUnitSphere * shakeAmount;

            tempShake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = origPos;
        }
    }

    public void ShakeCamera()
    {
        tempShake = shakeDuration;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cameraPosition;

    [SerializeField] private float shakeDuration = 0.2f;

    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;

    Vector3 origPos;
    bool shaking = false;
    void Start()
    {

    }

    // Update is called once per frame
    private float shakeTimer = 0f;
    void Update()
    {
        if (shaking)
        {
            shakeTimer += Time.deltaTime;
            transform.position = cameraPosition.position + Random.insideUnitSphere * shakeAmount;
            if (shakeTimer >= shakeDuration) { shaking = false; shakeTimer = 0f; }
        }
        else
            transform.position = cameraPosition.position;
    }

    public void CameraShake()
    {
        shaking = true;
    }

}

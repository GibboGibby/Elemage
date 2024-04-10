using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform worldPosition;
    [SerializeField] private Transform camera;

    [SerializeField] private float shakeDuration = 0.2f;
    private float tempShake;

    [SerializeField] private float shakeAmount = 0.7f;

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
            transform.position = worldPosition.position + Random.insideUnitSphere * shakeAmount;
            if (shakeTimer >= shakeDuration) { shaking = false; shakeTimer = 0f; }
        }
        else 
            transform.position = worldPosition.position;
        //transform.rotation = worldPosition.rotation;

        transform.LookAt(camera, Vector3.up);
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }

    public void Shake()
    {
        shaking = true;
    }
}

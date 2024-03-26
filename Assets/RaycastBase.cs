using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastBase : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool AbilityRaycast(string layer, float length, out RaycastHit hit)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer(layer);
        
        RaycastHit tempHit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out tempHit, length, mask))
        {
            hit = tempHit;
            return true;
        }
        hit = tempHit;
        return false;
    }

    

    public virtual void Fire()
    {
        Debug.Log("raycast fired");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform worldPosition;
    [SerializeField] private Transform camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = worldPosition.position;
        //transform.rotation = worldPosition.rotation;

        transform.LookAt(camera, Vector3.up);
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}

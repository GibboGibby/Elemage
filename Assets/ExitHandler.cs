using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupHandler.PickedUp)
        {
            PickupHandler.PickedUp = false;
            GameManager.Instance.GameOverWin();
        }
    }
}

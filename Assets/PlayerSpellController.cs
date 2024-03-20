using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject leftHandCanvas;
    [SerializeField] private GameObject rightHandCanvas;
    void Start()
    {
        if (playerController == null) playerController = GetComponent<PlayerController>();
        if (rb == null) rb = GetComponent<Rigidbody>();

        rightHandCanvas.SetActive(false);
        leftHandCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerController.enabled = false;
            Time.timeScale = 0.2f;
            rightHandCanvas.SetActive(true);
            leftHandCanvas.SetActive(true);
            //rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.enabled = true;
            Time.timeScale = 1.0f;
            rightHandCanvas.SetActive(false);
            leftHandCanvas.SetActive(false);
        }
    }
}

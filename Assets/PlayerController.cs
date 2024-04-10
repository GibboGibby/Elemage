using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool grounded;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float playerHeight;

    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;

    [SerializeField] private float airMultiplier;
    private float health;
    [SerializeField] private float maxHealth;
    

    private Quaternion initialRot;

    [Header("Movement")][SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float maxCameraAngleOnMove;
    [SerializeField] private float cameraLerpTime;

    [Header("Mouse Movement Settings")] private float xRotation;
    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float sensMultiplier = 1f;

    [Header("Keybinds")][SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Debug Stuff")]
    [SerializeField] private GameObject enemyPrefab;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private float desiredX;
    private float horizonalInput, verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        initialRot = transform.localRotation;
    }

    // Update is called once per frame

    public void ChangeSpeed(float amountToMult)
    {
        moveSpeed *= amountToMult;
        groundDrag *= amountToMult;
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        UpdateCamera();
        InputHandling();
        Look();

        if (Input.GetKeyDown(KeyCode.U))
        {
            RaycastHit hit;
            Physics.Raycast(playerCam.position, playerCam.forward, out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                Instantiate(enemyPrefab, hit.point + Vector3.up * 1.5f, Quaternion.identity);
            }
        }

        rb.drag = (grounded) ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizonalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void InputHandling()
    {
        horizonalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && grounded)
        {
            //readyToJump = false;
            Jump();
        }

    }

    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;


        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        Vector3 v = playerCam.transform.rotation.eulerAngles;
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, v.z);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void UpdateCamera()
    {
        float rotZ = -horizonalInput * maxCameraAngleOnMove;

        Quaternion finalRot = Quaternion.Euler(xRotation, desiredX, rotZ);
        playerCam.transform.localRotation =
        Quaternion.RotateTowards(playerCam.transform.localRotation, finalRot, cameraLerpTime);
    }

    private void Jump()
    {

    }

    public void PlayerHit(float damage)
    {
        health -= damage;
        Camera.main.transform.parent.GetComponent<MoveCamera>().CameraShake();
        if (health <= 0)
        {
            Debug.Log("Game Over");
            GameManager.Instance.GameOver();
        }
    }
}

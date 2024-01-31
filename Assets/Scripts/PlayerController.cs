using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public static PlayerController instance;

    private CharacterController controller;
    public bool isMovementEnabled = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isMovementEnabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            moveDirection.y -= 9.81f * Time.deltaTime;

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
}

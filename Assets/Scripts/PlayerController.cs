using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float squatDuration = 0.2f;
    public float squatDepth = 2f;
    public float moveSpeed = 5.0f;
    public bool isCrouch = false;
    public GameObject playerCamera;
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
        if (isMovementEnabled) MovePlayer();
        if (Input.GetKeyDown(KeyCode.LeftControl)) StartCoroutine(Crouch());
    }
    private void Awake()
    {
        instance = this;
        StartCoroutine(Crouch());
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        moveDirection.y -= 9.81f * Time.deltaTime;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    private IEnumerator Crouch()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = playerCamera.transform.position;
        Vector3 targetPosition = initialPosition;

        if (isCrouch) targetPosition = initialPosition - new Vector3(0f, squatDepth, 0f);
        if (!isCrouch) targetPosition = initialPosition + new Vector3(0f, squatDepth, 0f);

        while (elapsedTime < squatDuration)
        {
            playerCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / squatDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        playerCamera.transform.position = targetPosition;

        isCrouch = !isCrouch;
        yield return null;
    }
}

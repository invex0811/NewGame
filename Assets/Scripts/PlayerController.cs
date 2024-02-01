using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private CharacterController _controller;

    public GameObject PlayerBody;
    public float SquatDuration = 0.5f;
    public float SquatDepth = 2f;
    public float MoveSpeed = 15.0f;
    public bool IsMovementEnabled = true;
    public bool IsCrouchEnabled = true;
    public bool IsCrouch = false;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        if (IsMovementEnabled) MovePlayer();
        if (Input.GetKeyDown(KeyCode.LeftControl) && IsCrouchEnabled) StartCoroutine(Crouch());
    }
    private void Awake()
    {
        Instance = this;
    }

    public void EnableMovement()
    {
        IsMovementEnabled = true;
    }

    public void DisableMovement()
    {
        IsMovementEnabled = false;
    }
    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Ёту залупу добавил GPT как € пон€л. я так и не смог пон€ть наху€.
        //moveDirection.y -= 9.81f * Time.deltaTime;

        _controller.Move(MoveSpeed * Time.deltaTime * moveDirection);
    }
    private IEnumerator Crouch()
    {
        IsCrouchEnabled = false;

        float elapsedTime = 0f;
        float targetScale = PlayerBody.transform.localScale.y;
        if (!IsCrouch) targetScale -= SquatDepth;
        if (IsCrouch) targetScale += SquatDepth;

        while (elapsedTime < SquatDuration)
        {
            PlayerBody.transform.localScale = Vector3.Lerp(PlayerBody.transform.localScale, new Vector3(PlayerBody.transform.localScale.x, targetScale, PlayerBody.transform.localScale.z), elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayerBody.transform.localScale = new Vector3(PlayerBody.transform.localScale.x, targetScale, PlayerBody.transform.localScale.z);

        IsCrouch = !IsCrouch;
        IsCrouchEnabled = true;

        yield return null;
    }
}

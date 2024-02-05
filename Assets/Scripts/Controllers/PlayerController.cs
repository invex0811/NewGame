using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _squatDuration = 0.5f;
    private float _squatDepth = 2f;
    private bool _isCrouchEnabled = true;
    private bool _isCrouch = false;
    private CharacterController _controller;

    public static PlayerController Instance;

    public GameObject PlayerBody;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    private void Update()
    {
        if (GameManager.TypeOfControl != TypesOfControl.PlayerControl)
        {
            Debug.LogError("Type of controll conflict.");
            return;
        }

        if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Crouch]) && _isCrouchEnabled)
            StartCoroutine(Crouch());

        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        _controller.Move(Player.MoveSpeed * Time.deltaTime * moveDirection);
    }
    private IEnumerator Crouch()
    {
        _isCrouchEnabled = false;

        float elapsedTime = 0f;
        float targetScale = PlayerBody.transform.localScale.y;
        if (!_isCrouch)
            targetScale -= _squatDepth;
        if (_isCrouch)
            targetScale += _squatDepth;

        while (elapsedTime < _squatDuration)
        {
            PlayerBody.transform.localScale = Vector3.Lerp(PlayerBody.transform.localScale, new Vector3(PlayerBody.transform.localScale.x, targetScale, PlayerBody.transform.localScale.z), elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayerBody.transform.localScale = new Vector3(PlayerBody.transform.localScale.x, targetScale, PlayerBody.transform.localScale.z);

        _isCrouch = !_isCrouch;
        _isCrouchEnabled = true;

        yield return null;
    }
}
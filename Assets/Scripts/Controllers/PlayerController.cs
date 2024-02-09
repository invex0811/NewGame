using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _playerBody;

    private CharacterController _controller;
    private float _squatDuration = 0.5f;
    private float _squatDepth = 2f;
    private bool _isCrouchEnabled = true;
    private bool _isCrouch = false;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (GameManager.TypeOfControl != TypesOfControl.PlayerControl)
        {
            Debug.LogError("Type of controll conflict.");
            Debug.Log(GameManager.TypeOfControl);
            return;
        }

        if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Crouch]) && _isCrouchEnabled)
            StartCoroutine(Crouch());

        MovePlayer();
    }
    private void OnEnable()
    {
        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
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
        float targetScale = _playerBody.transform.localScale.y;
        if (!_isCrouch)
            targetScale -= _squatDepth;
        if (_isCrouch)
            targetScale += _squatDepth;

        while (elapsedTime < _squatDuration)
        {
            _playerBody.transform.localScale = Vector3.Lerp(_playerBody.transform.localScale, new Vector3(_playerBody.transform.localScale.x, targetScale, _playerBody.transform.localScale.z), elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _playerBody.transform.localScale = new Vector3(_playerBody.transform.localScale.x, targetScale, _playerBody.transform.localScale.z);

        _isCrouch = !_isCrouch;
        _isCrouchEnabled = true;

        yield return null;
    }
}
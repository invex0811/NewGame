using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _flashlight;
    [SerializeField] private float _staminaRegenerationDelay;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _walkSpeed;

    private CharacterController _controller;
    private Coroutine _stamninaReducingRoutine;
    private Coroutine _stamninaRegeneratingRoutine;
    private int _currentClipIndex;
    private bool _isFootstepSoundPlaying = false;
    private bool _isSprinting = false;
    private float _moveSpeed = 20f;
    private float _stamina = 100;

    public static PlayerController Instance;

    public float Stamina
    {
        get => _stamina;
    }

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

        if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.ToogleFlashlight]))
        {
            _flashlight.SetActive(!_flashlight.activeSelf);
            GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), _audioSource);
        }

        if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Sprint]))
            EnableSprint();
        if (Input.GetKeyUp(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Sprint]))
            DisableSprint();

        MovePlayer();
    }
    private void FixedUpdate()
    {
        Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 7);

        if (hit.collider == null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            return;
        }

        transform.position = new Vector3(transform.position.x, hit.collider.transform.position.y + 6.9f, transform.position.z);
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

        _controller.Move(_moveSpeed * Time.deltaTime * moveDirection);

        if (horizontalInput == 1 || horizontalInput == -1 || verticalInput == 1 || verticalInput == -1)
        {
            if (_isFootstepSoundPlaying)
                return;

            StartCoroutine(PlayFootstepSound());
        }
    }
    private void EnableSprint()
    {
        if (_stamina <= 20 || _isSprinting)
            return;

        Debug.Log("Sprint enabled");

        _moveSpeed = _sprintSpeed;
        _isSprinting = true;

        if (_stamninaReducingRoutine != null)
            StopCoroutine(_stamninaReducingRoutine);
        if (_stamninaRegeneratingRoutine != null)
            StopCoroutine(_stamninaRegeneratingRoutine);

        _stamninaReducingRoutine = StartCoroutine(ReduceStamina());
    }
    private void DisableSprint()
    {
        if (!_isSprinting)
            return;

        Debug.Log("Sprint disabled");

        _moveSpeed = _walkSpeed;
        _isSprinting = false;

        if (_stamninaReducingRoutine != null)
            StopCoroutine(_stamninaReducingRoutine);
        if (_stamninaRegeneratingRoutine != null)
            StopCoroutine(_stamninaRegeneratingRoutine);

        _stamninaRegeneratingRoutine = StartCoroutine(RegenerateStamina());
    }
    private IEnumerator PlayFootstepSound()
    {
        int clipIndex = Random.Range(0, _stepSounds.Length - 1);

        _isFootstepSoundPlaying = true;

        while(clipIndex == _currentClipIndex)
        {
            clipIndex = Random.Range(0, _stepSounds.Length - 1);
        }

        _currentClipIndex = clipIndex;

        _audioSource.clip = _stepSounds[clipIndex];
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);

        _audioSource.Stop();
        _isFootstepSoundPlaying = false;

        yield break;
    }
    private IEnumerator ReduceStamina()
    {
        while (_stamina != 0)
        {
            _stamina--;
            Debug.Log($"Stamina reduced: {_stamina}");

            yield return new WaitForSeconds(0.1f);
        }

        DisableSprint();

        yield break;
    }
    private IEnumerator RegenerateStamina()
    {
        Debug.Log($"Stamina regeneration delay started");
        yield return new WaitForSeconds(_staminaRegenerationDelay);

        while(_stamina < 100)
        {
            _stamina++;
            Debug.Log($"Stamina regenerated: {_stamina}");
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
}
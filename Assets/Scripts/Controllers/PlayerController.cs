using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _flashlight;
    
    private CharacterController _controller;
    private float _squatDuration = 0.5f;
    private float _squatDepth = 2f;
    private bool _isCrouchEnabled = true;
    private bool _isCrouch = false;
    private bool _isFootstepSoundPlaying = false;
    private int _currentClipIndex;

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

        if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.ToogleFlashlight]))
        {
            _flashlight.SetActive(!_flashlight.activeSelf);
            GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), _audioSource);
        }

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

        if (horizontalInput == 1 || horizontalInput == -1 || verticalInput == 1 || verticalInput == -1)
        {
            if (_isFootstepSoundPlaying)
                return;

            StartCoroutine(PlayFootstepSound());
        }
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
    private IEnumerator Crouch()
    {
        _isCrouchEnabled = false;

        float elapsedTime = 0f;
        float targetScale = transform.localScale.y;
        if (!_isCrouch)
            targetScale -= _squatDepth;
        if (_isCrouch)
            targetScale += _squatDepth;

        while (elapsedTime < _squatDuration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, targetScale, transform.localScale.z), elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(transform.localScale.x, targetScale, transform.localScale.z);

        _isCrouch = !_isCrouch;
        _isCrouchEnabled = true;

        yield return null;
    }
}
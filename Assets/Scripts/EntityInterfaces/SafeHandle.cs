using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeHandleController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Tooltip("Degrees per frame.")] private float _rotationSpeed;
    [SerializeField] private float _rotationAngleWhenLocked;
    [SerializeField] private float _rotationAngleWhenUnlocked;
    [SerializeField] private Axis _rotationAxis;

    private SafeDoor _door;
    private float _deltaTime = 0;
    private float _rotationTime;

    private void OnEnable()
    {
        _door = GetComponentInParent<SafeDoor>();
    }
    private void OnDisable()
    {
        _door = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_deltaTime != 0)
            return;
        StartCoroutine(RotateHandle());

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), gameObject.GetComponent<AudioSource>());
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe/Textures/SafeMatHighlighted");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe/Textures/SafeMat");
    }

    private IEnumerator RotateHandle()
    {
        Vector3 axis = _rotationAxis switch
        {
            Axis.X => Vector3.right,
            Axis.Y => Vector3.up,
            Axis.Z => Vector3.forward,
            Axis.NegativeX => Vector3.left,
            Axis.NegativeY => Vector3.down,
            Axis.NegativeZ => Vector3.back,
            _ => Vector3.right,
        };

        if (_door.IsLocked)
        {
            _rotationTime = _rotationAngleWhenLocked / _rotationSpeed;

            while (_deltaTime < _rotationTime)
            {
                gameObject.transform.Rotate(axis, _rotationSpeed);
                _deltaTime++;
                yield return new WaitForFixedUpdate();
            }

            _deltaTime = 0;

            while (_deltaTime < _rotationTime)
            {
                gameObject.transform.Rotate(-axis, _rotationSpeed);
                _deltaTime++;
                yield return new WaitForFixedUpdate();
            }

            OnHandleEndRotation?.Invoke();
        }

        if (!_door.IsLocked)
        {
            _rotationTime = _rotationAngleWhenUnlocked / _rotationSpeed;

            while (_deltaTime < _rotationTime)
            {
                gameObject.transform.Rotate(axis, _rotationSpeed);
                _deltaTime++;
                yield return new WaitForFixedUpdate();
            }
            OnHandleEndRotation?.Invoke();

            GetComponentInParent<Entity>().gameObject.layer = 0;
        }

        _deltaTime = 0;
        yield break;
    }

    public delegate void HandleEndRotationEventHandler();
    public event HandleEndRotationEventHandler OnHandleEndRotation;
}
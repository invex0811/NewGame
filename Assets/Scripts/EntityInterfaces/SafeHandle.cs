using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeHandleController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationDuration;

    private SafeDoor _door;
    private float _deltaTime = 0;

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

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], gameObject.GetComponent<AudioSource>());
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
        if (_door.IsLocked)
        {
            while (_deltaTime < _rotationDuration / 12)
            {
                gameObject.transform.Rotate(_rotationSpeed, 0, 0);
                _deltaTime++;
                yield return null;
            }

            _deltaTime = 0;

            while (_deltaTime < _rotationDuration / 12)
            {
                gameObject.transform.Rotate(-_rotationSpeed, 0, 0);
                _deltaTime++;
                yield return null;
            }
        }

        if (!_door.IsLocked)
        {
            while (_deltaTime < _rotationDuration)
            {
                gameObject.transform.Rotate(_rotationSpeed, 0, 0);
                _deltaTime++;
                yield return null;
            }
            OnHandleEndRotation?.Invoke();

            GetComponentInParent<EntityID>().gameObject.layer = 0;
        }

        _deltaTime = 0;
        yield break;
    }

    public delegate void HandleEndRotationEventHandler();
    public event HandleEndRotationEventHandler OnHandleEndRotation;
}
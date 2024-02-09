using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeHandleController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SafeDoorController _door;
    private float _deltaTime = 0;

    private void OnEnable()
    {
        _door = GetComponentInParent<SafeDoorController>();
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
            while (_deltaTime < 30)
            {
                gameObject.transform.Rotate(2f, 0, 0);
                _deltaTime++;
                yield return null;
            }
            while (_deltaTime < 60)
            {
                gameObject.transform.Rotate(-2f, 0, 0);
                _deltaTime++;
                yield return null;
            }
        }

        if (!_door.IsLocked)
        {
            while (_deltaTime < 180)
            {
                gameObject.transform.Rotate(2f, 0, 0);
                _deltaTime++;
                yield return null;
            }
            OnHandleEndRotation?.Invoke();
        }

        _deltaTime = 0;
        yield break;
    }

    public delegate void HandleEndRotationEventHandler();
    public event HandleEndRotationEventHandler OnHandleEndRotation;
}
using UnityEngine;
using System.Collections.Generic;

public class CameraInteraction : MonoBehaviour
{
    private Dictionary<GameObject, CameraState> _interactionStates = new Dictionary<GameObject, CameraState>();
    private GameObject _currentIndicatorSpriteObject;

    public GameObject IndicatorSpritePrefab;
    public Camera MainCamera;
    public Camera InteractionCamera;
    public KeyCode InteractionKey = KeyCode.E;
    public KeyCode CancelKey = KeyCode.Escape;
    public float InteractionDistance = 7f;
    public bool IsInteractionStarted = false;
    public bool IsSpriteSpawned = false;

    private class CameraState
    {
        public Vector3 objectPosition; // Добавлено для сохранения позиции объекта
        public Quaternion objectRotation; // Добавлено для сохранения поворота объекта
    }

    private void Update()
    {
        CheckInteractionDistance();

        if (!IsSpriteSpawned) return;

        if (Input.GetKeyDown(InteractionKey) && !IsInteractionStarted)
        {
            _interactionStates[_currentIndicatorSpriteObject] = new CameraState
            {
                objectPosition = _currentIndicatorSpriteObject.transform.position,
                objectRotation = _currentIndicatorSpriteObject.transform.rotation
            };

            MoveToInteractionPoint();
            GameManager.TogglePause();
            GameManager.DisableCameraControl(MainCamera);
            GameManager.DisablePlayerControl();
            IsInteractionStarted = true;
            return;
        }

        if (IsInteractionStarted && Input.GetKeyDown(CancelKey))
        {
            ReturnToInteractionPosition();
            GameManager.TogglePause();
            GameManager.EnableCameraControl(MainCamera);
            GameManager.EnablePlayerControl();
            IsInteractionStarted = false;
            return;
        }
    }

    private void ToogleIntercationCamera()
    {
        if (MainCamera == null || InteractionCamera == null) return;

        InteractionCamera.enabled = !InteractionCamera.enabled;
        MainCamera.enabled = !MainCamera.enabled;

        CameraController controller = MainCamera.GetComponent<CameraController>();
        controller.enabled = MainCamera.enabled;
    }
    private void MoveToInteractionPoint()
    {
        if (_currentIndicatorSpriteObject == null && !_interactionStates.ContainsKey(_currentIndicatorSpriteObject)) return;

        ToogleIntercationCamera();

        InteractionCamera.transform.position = _interactionStates[_currentIndicatorSpriteObject].objectPosition;
        Vector3 objectEulerAngles = _interactionStates[_currentIndicatorSpriteObject].objectRotation.eulerAngles;
        InteractionCamera.transform.rotation = Quaternion.Euler(0f, objectEulerAngles.y, 0f);
    }
    private void ReturnToInteractionPosition()
    {
        if (_currentIndicatorSpriteObject == null && !_interactionStates.ContainsKey(_currentIndicatorSpriteObject)) return;

        ToogleIntercationCamera();
    }
    private void CheckInteractionDistance()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractionDistance);

        foreach (var collider in colliders)
        {
            GameObject obj = collider.gameObject;

            float distance = Vector3.Distance(obj.transform.position, MainCamera.transform.position);

            if (distance <= InteractionDistance)
            {
                ShowSprite(transform.position, transform.rotation);
                return;
            }
        }

        HideSprite();
    }
    private void ShowSprite(Vector3 position, Quaternion rotation)
    {
        if (IsSpriteSpawned) return;

        if (IndicatorSpritePrefab == null)
        {
            Debug.LogError("Префаб индикатора спрайта не назначен в инспекторе!");
            return;
        }

        _currentIndicatorSpriteObject = Instantiate(IndicatorSpritePrefab, position, rotation);
        IsSpriteSpawned = true;

        CameraState state = new CameraState
        {
            objectPosition = position,
            objectRotation = rotation
        };

        _interactionStates[_currentIndicatorSpriteObject] = state;
    }
    private void HideSprite()
    {
        if (_currentIndicatorSpriteObject != null)
        {
            Destroy(_currentIndicatorSpriteObject);
            IsSpriteSpawned = false;
        }
    }
}
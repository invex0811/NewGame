using UnityEngine;
using System.Collections.Generic;

public class CameraInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode cancelKey = KeyCode.Escape;
    public float interactionDistance = 7f;
    public GameObject indicatorSpritePrefab;

    private Dictionary<GameObject, CameraState> interactionStates = new Dictionary<GameObject, CameraState>();
    private GameObject currentIndicatorSpriteObject;
    private bool isSpriteSpawned = false;
    private bool isInteractionStarted = false;

    private class CameraState
    {
        public Vector3 cameraPosition;
        public Quaternion cameraRotation;
        public Vector3 objectPosition; // ��������� ��� ���������� ������� �������
        public Quaternion objectRotation; // ��������� ��� ���������� �������� �������
    }

    void Update()
    {
        CheckInteractionDistance();

        if (isSpriteSpawned)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                interactionStates[currentIndicatorSpriteObject] = new CameraState
                {
                    cameraPosition = Camera.main.transform.position,
                    cameraRotation = Camera.main.transform.rotation,
                    objectPosition = currentIndicatorSpriteObject.transform.position,
                    objectRotation = currentIndicatorSpriteObject.transform.rotation
                };

                MoveCameraToInteractionPoint();
                GameManager.TogglePause();
                GameManager.DisableCameraControl();
                GameManager.DisablePlayerControl();
                isInteractionStarted = true;
            }
            else if (isInteractionStarted && Input.GetKeyDown(cancelKey))
            {
                ReturnCameraToInteractionPosition();
                GameManager.TogglePause();
                GameManager.EnableCameraControl();
                GameManager.EnablePlayerControl();
                isInteractionStarted = false;
            }
        }
    }

    void MoveCameraToInteractionPoint()
    {
        if (currentIndicatorSpriteObject != null && interactionStates.ContainsKey(currentIndicatorSpriteObject))
        {
            Camera.main.transform.position = interactionStates[currentIndicatorSpriteObject].objectPosition;

            // ������������ ������ �� ����������� �������
            Vector3 objectEulerAngles = interactionStates[currentIndicatorSpriteObject].objectRotation.eulerAngles;
            Camera.main.transform.rotation = Quaternion.Euler(0f, objectEulerAngles.y, 0f);

            // �������������� ������ ��������������, ���� ����������
        }
    }

    void ReturnCameraToInteractionPosition()
    {
        if (currentIndicatorSpriteObject != null && interactionStates.ContainsKey(currentIndicatorSpriteObject))
        {
            CameraState state = interactionStates[currentIndicatorSpriteObject];
            Camera.main.transform.position = state.cameraPosition;
            Camera.main.transform.rotation = state.cameraRotation;
        }
    }

    void ShowSprite(Vector3 position, Quaternion rotation)
    {
        if (indicatorSpritePrefab != null && !isSpriteSpawned)
        {
            currentIndicatorSpriteObject = Instantiate(indicatorSpritePrefab, position, rotation);
            isSpriteSpawned = true;

            CameraState state = new CameraState
            {
                cameraPosition = Camera.main.transform.position,
                cameraRotation = Camera.main.transform.rotation,
                objectPosition = position,
                objectRotation = rotation
            };

            interactionStates[currentIndicatorSpriteObject] = state;
        }
        else if (indicatorSpritePrefab == null)
        {
            Debug.LogError("������ ���������� ������� �� �������� � ����������!");
        }
    }

    void HideSprite()
    {
        if (currentIndicatorSpriteObject != null)
        {
            Destroy(currentIndicatorSpriteObject);
            isSpriteSpawned = false;
        }
    }

    void CheckInteractionDistance()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);

        foreach (var collider in colliders)
        {
            GameObject obj = collider.gameObject;

            float distance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);

            if (distance <= interactionDistance)
            {
                ShowSprite(obj.transform.position, obj.transform.rotation);
                return;
            }
        }

        HideSprite();
    }

}

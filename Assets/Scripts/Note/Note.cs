using System.Collections;
using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasPanel;
    public GameObject modelContainer;
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode rotateKey = KeyCode.R;
    public PauseUI pauseUI;
    public float interactionDistance = 7f;
    public GameObject indicatorSpritePrefab;
    private GameObject currentIndicatorSpriteObject;
    private bool isPaused = false;
    private bool isInRange = false;
    private bool isCameraControlEnabled = true;
    private bool isInterfaceVisible = false;
    private bool isMouse1Pressed = false;
    private GameObject interfaceObject;
    public GameObject yourModelPrefab;
    private Vector3 initialModelPosition;
    private Quaternion initialModelRotation;
    private GameObject currentInteractionObject;
    private GameObject currentInteractionModel;
    private Vector3 spritePositionOffset = new Vector3(0f, 1f, 0f);
    private Vector3 spriteRotation = new Vector3(0f, 180f, 0f);
    public float yourDesiredDistance = 3f;

    // ƒобавленные переменные дл€ отслеживани€ спрайта
    private bool isSpriteSpawned = false;

    void Update()
    {
        CheckInteractionDistance();

        if (Input.GetKeyDown(interactionKey) && !isPaused && isInRange)
        {
            PauseGame();
            if (!isInterfaceVisible && currentInteractionObject != null)
            {
                Show3DModel(currentInteractionObject.GetComponent<NoteInteraction>().yourModelPrefab, 1);
                isInterfaceVisible = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
            if (isInterfaceVisible)
            {
                Hide3DModel();
                isInterfaceVisible = false;
                currentInteractionObject = null;
            }
        }

        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0 && currentInteractionModel != null)
        {
            RotateModel(scrollDelta);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isMouse1Pressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouse1Pressed = false;
        }

        if (isMouse1Pressed && currentInteractionModel != null)
        {
            RotateModelXYZ();
        }

        if (Input.GetKeyDown(rotateKey) && currentInteractionModel != null)
        {
            currentInteractionModel.transform.position = initialModelPosition;
            currentInteractionModel.transform.rotation = initialModelRotation;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
            Hide3DModel();
            isInterfaceVisible = false;
        }

        if (isCameraControlEnabled)
        {
            // ¬аши текущие действи€ по управлению камерой здесь
        }
    }

    void CheckInteractionDistance()
    {
        GameObject[] interactionZones = GameObject.FindGameObjectsWithTag("InteractionZone");

        bool foundInRange = false;

        foreach (var zone in interactionZones)
        {
            float distance = Vector3.Distance(zone.transform.position, player.transform.position);

            if (distance <= interactionDistance)
            {
                ShowSprite(zone.transform.position);
                foundInRange = true;
                currentInteractionObject = zone;
            }
        }

        if (!foundInRange)
        {
            isInRange = false;
            Hide3DModel();
            if (isSpriteSpawned)
            {
                HideSprite();
            }
            currentInteractionObject = null;
        }
        else
        {
            isInRange = true;
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseUI.TogglePauseUI(true);

        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.enabled = false;
            }
            else
            {
                Debug.LogError("PlayerController script not found on player object!");
            }

            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                CameraController cameraController = mainCamera.GetComponent<CameraController>();

                if (cameraController != null)
                {
                    cameraController.enabled = false;
                    isCameraControlEnabled = false;
                }
                else
                {
                    Debug.LogError("CameraController script not found on the object with tag 'Main Camera'!");
                }
            }
            else
            {
                Debug.LogError("Main Camera not found!");
            }
        }
        else
        {
            Debug.LogError("Player object is null!");
        }
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.TogglePauseUI(false);

        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerController script not found on player object!");
            }

            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                CameraController cameraController = mainCamera.GetComponent<CameraController>();

                if (cameraController != null)
                {
                    cameraController.enabled = true;
                    isCameraControlEnabled = true;
                }
                else
                {
                    Debug.LogError("CameraController script not found on the object with tag 'Main Camera'!");
                }
            }
            else
            {
                Debug.LogError("Main Camera not found!");
            }
        }
        else
        {
            Debug.LogError("Player object is null!");
        }
    }

    void HideInterface(bool destroyObject = false)
    {
        if (destroyObject && interfaceObject != null)
        {
            Destroy(interfaceObject);
        }
    }

    void ShowSprite(Vector3 position)
    {
        if (!isSpriteSpawned)
        {
            // ≈сли спрайт еще не создан, создаем новый
            currentIndicatorSpriteObject = Instantiate(indicatorSpritePrefab, position + spritePositionOffset, Quaternion.Euler(spriteRotation));
            currentIndicatorSpriteObject.tag = "IndicatorSprite";
            currentIndicatorSpriteObject.SetActive(!isPaused);
            isSpriteSpawned = true;
        }
        else
        {
            currentIndicatorSpriteObject.transform.position = position + spritePositionOffset;
            currentIndicatorSpriteObject.SetActive(!isPaused);
        }
    }

    void HideSprite()
    {
        if (currentIndicatorSpriteObject != null)
        {
            currentIndicatorSpriteObject.SetActive(false);
        }
    }

    void Show3DModel(GameObject modelPrefab, int modelSortingOrder)
    {
        if (modelContainer != null && modelPrefab != null)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                // ѕровер€ем, есть ли уже клон в контейнере
                if (modelContainer.transform.childCount > 0)
                {
                    // ≈сли есть, удал€ем его
                    Destroy(modelContainer.transform.GetChild(0).gameObject);
                }

                Vector3 cameraPosition = mainCamera.transform.position;
                Vector3 cameraForward = mainCamera.transform.forward;

                float distanceToFrontOfPanel = 0f;

                Vector3 modelPosition = cameraPosition + cameraForward * (yourDesiredDistance + distanceToFrontOfPanel);

                // —оздаем новый клон
                currentInteractionModel = Instantiate(modelPrefab, modelPosition, Quaternion.identity);
                currentInteractionModel.transform.SetParent(modelContainer.transform);

                initialModelPosition = modelPosition;
                initialModelRotation = Quaternion.LookRotation(-cameraForward);

                currentInteractionModel.transform.SetAsLastSibling();

                Canvas modelCanvas = currentInteractionModel.GetComponent<Canvas>();
                if (modelCanvas == null)
                {
                    modelCanvas = currentInteractionModel.AddComponent<Canvas>();
                }

                if (modelCanvas != null)
                {
                    modelCanvas.sortingOrder = modelSortingOrder;
                    modelCanvas.worldCamera = mainCamera;
                }
                else
                {
                    Debug.LogError("Canvas component not found or added on the 3D model prefab!");
                }

                currentInteractionModel.transform.rotation = initialModelRotation;
            }
            else
            {
                Debug.LogError("Main Camera not found!");
            }
        }
        else
        {
            Debug.LogError("Model container or model prefab is not assigned!");
        }
    }

    void Hide3DModel()
    {
        if (currentInteractionModel != null)
        {
            Destroy(currentInteractionModel);
        }
    }

    void RotateModel(float scrollDelta)
    {
        float rotationSpeed = 5f;
        currentInteractionModel.transform.Rotate(Vector3.up, scrollDelta * rotationSpeed, Space.World);
    }

    void RotateModelXYZ()
    {
        float rotationSpeed = 5f;
        float rotationX = Input.GetAxis("Mouse Y") * rotationSpeed;
        float rotationY = Input.GetAxis("Mouse X") * rotationSpeed;

        currentInteractionModel.transform.Rotate(rotationX, rotationY, 0, Space.World);
    }
}
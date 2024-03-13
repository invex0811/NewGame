using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectInspectorController : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject _inspectionPanel;
    [SerializeField] private Camera _inspectionCamera;
    [SerializeField] private Button _closeInspectionPanelButton;
    [SerializeField] private TextMeshProUGUI _objectName;
    [SerializeField] private TextMeshProUGUI _objectDescription;

    private GameObject _inspectablePrefab;
    private Quaternion _initialEntityRotation;
    private float _currentZoom;

    public static ObjectInspectorController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]) || Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.AlternativeCloseInventory]))
            CloseInspectionPanel();
        if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.ResetInspectionPanel]))
            ResetPanel();
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            ZoomIn();
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            ZoomOut();
    }

    private void OnEnable()
    {
        GameManager.PauseGame();

        PlayerController.Instance.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _closeInspectionPanelButton.onClick.AddListener(() => CloseInspectionPanel());

        _currentZoom = 0;
    }
    private void OnDisable()
    {
        _closeInspectionPanelButton.onClick.RemoveAllListeners();
    }

    private void CloseInspectionPanel()
    {
        _inspectionPanel.SetActive(false);
        _inspectionCamera.enabled = false;

        if(GameManager.TypeOfControl == TypesOfControl.InspectionControll)
        {
            GameManager.ResumeGame();
            GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
            PlayerController.Instance.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());

        enabled = false;
    }
    private void ResetPanel()
    {
        _inspectablePrefab.transform.rotation = _initialEntityRotation;
        _currentZoom = 0;
        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    private void ZoomIn()
    {
        _currentZoom += 0.1f;

        if (_currentZoom > 2.5f)
            _currentZoom = 2.5f;

        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    private void ZoomOut()
    {
        _currentZoom -= 0.1f;

        if (_currentZoom < 0)
            _currentZoom = 0;

        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    public void Initialize(EntityType type)
    {
        _inspectionPanel.SetActive(true);
        Entity item = EntitiesList.Entities[type];

        if (_inspectablePrefab != null)
            Destroy(_inspectablePrefab);

        _inspectablePrefab = Instantiate(item.Prefab, new Vector3(1000, 1000, 1000), new Quaternion(0, 180, 0, 0));
        _initialEntityRotation = _inspectablePrefab.transform.rotation;

        _inspectionCamera.enabled = true;
        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995);
        _inspectionCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        _objectName.text = item.DisplayName;
        _objectDescription.text = item.Description;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_inspectablePrefab != null)
        {
            _inspectablePrefab.transform.Rotate(Vector3.left, eventData.delta.y / 3);
            _inspectablePrefab.transform.Rotate(Vector3.down, eventData.delta.x / 3);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectInspectorController : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject _inspectionPanel;
    [SerializeField] private Camera _inspectionCamera;
    [SerializeField] private Button _closeInspectionPanelButton;
    [SerializeField] private TextMeshProUGUI _entityName;
    [SerializeField] private TextMeshProUGUI _entityDescription;

    private GameObject _entityPrefab;
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

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());

        enabled = false;
    }
    private void ResetPanel()
    {
        _entityPrefab.transform.rotation = _initialEntityRotation;
        _currentZoom = 0;
        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    private void ZoomIn()
    {
        _currentZoom += 0.1f;

        if (_currentZoom > 2.5f)
            _currentZoom = 2.5f;

        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    private void ZoomOut()
    {
        _currentZoom -= 0.1f;

        if (_currentZoom < 0)
            _currentZoom = 0;

        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995 + _currentZoom);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    public void Initialize(Entity entity)
    {
        _inspectionPanel.SetActive(true);

        if (_entityPrefab != null)
            Destroy(_entityPrefab);

        _entityPrefab = Instantiate(entity.EntityScriptableObject.Prefab, new Vector3(1000, 1000, 1000), new Quaternion(0, 180, 0, 0));
        _initialEntityRotation = _entityPrefab.transform.rotation;

        _inspectionCamera.enabled = true;
        _inspectionCamera.transform.position = new Vector3(1000, 1000, 995);
        _inspectionCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        _entityName.text = entity.EntityScriptableObject.Name;

        if (entity is Item item)
        {
            _entityDescription.text = item.ItemScriptableObject.Description;
            return;
        }

        _entityDescription.text = "";
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_entityPrefab != null)
        {
            _entityPrefab.transform.Rotate(Vector3.left, eventData.delta.y / 3);
            _entityPrefab.transform.Rotate(Vector3.down, eventData.delta.x / 3);
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _inspectButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private Button _cancelButton;

    public static OptionsPanelController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        InventoryController.Instance.OnPanelChange += ClosePanel;
    }
    private void OnDisable()
    {
        InventoryController.Instance.OnPanelChange -= ClosePanel;
        ClosePanel();
    }

    public void OpenPanel(Entity entity)
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        if (entity is Document)
        {
            _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Read";
            _useButton.onClick.AddListener(() => Read(entity));
            _discardButton.gameObject.SetActive(false);
        }
        else if (entity is Item)
        {
            _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            _useButton.onClick.AddListener(() => Use(entity));
            _discardButton.gameObject.SetActive(true);
            _discardButton.onClick.AddListener(() => Discard(entity));
            Debug.Log("1");
        }
        else
        {
            throw new Exception("Invalid type of entity");
        }

        _inspectButton.onClick.AddListener(() => Inspect(entity));
        _cancelButton.onClick.AddListener(() => ClosePanel());

        _optionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, mousePosition, _parentCanvas.worldCamera, out Vector2 localMousePosition);
        _optionsPanel.transform.position = _parentCanvas.transform.TransformPoint(localMousePosition);

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    private void Use(Entity entity)
    {
        Item item = entity as Item;
        item.Use();
        ClosePanel();
    }
    private void Read(Entity entity)
    {
        Document document = entity as Document;
        document.Read();
        ClosePanel();
    }
    private void Inspect(Entity entity)
    {
        ObjectInspectorController.Instance.enabled = true;
        ObjectInspectorController.Instance.Initialize(entity);

        ClosePanel();
    }
    private void Discard(Entity entity)
    {
        Item item = entity as Item;
        Player.Inventory.Remove(item);
        ClosePanel();
    }
    private void ClosePanel()
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());

        _optionsPanel.SetActive(false);
    }
}
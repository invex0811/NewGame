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

    public void OpenPanel(EntityType type)
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        if (EntitiesList.Entities[type] is Document)
        {
            _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Read";
            _useButton.onClick.AddListener(() => Read(type));
            _discardButton.gameObject.SetActive(false);
        }
        else if (EntitiesList.Entities[type] is Item)
        {
            _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            _useButton.onClick.AddListener(() => Use(type));
            _discardButton.gameObject.SetActive(true);
            _discardButton.onClick.AddListener(() => Discard(type));
            Debug.Log("1");
        }
        else
        {
            throw new Exception("Invalid type of entity");
        }

        _inspectButton.onClick.AddListener(() => Inspect(type));
        _cancelButton.onClick.AddListener(() => ClosePanel());

        _optionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, mousePosition, _parentCanvas.worldCamera, out Vector2 localMousePosition);
        _optionsPanel.transform.position = _parentCanvas.transform.TransformPoint(localMousePosition);

        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    private void Use(EntityType type)
    {
        Item item = EntitiesList.Entities[type] as Item;
        item.Use();
        ClosePanel();
    }
    private void Read(EntityType type)
    {
        Document document = EntitiesList.Entities[type] as Document;
        document.Read();
        ClosePanel();
    }
    private void Inspect(EntityType type)
    {
        ObjectInspectorController.Instance.enabled = true;
        ObjectInspectorController.Instance.Initialize(type);

        ClosePanel();
    }
    private void Discard(EntityType type)
    {
        Item item = EntitiesList.Entities[type] as Item;
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
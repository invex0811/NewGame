using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    public void OpenPanel(Item item)
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use";

        _discardButton.gameObject.SetActive(true);

        _useButton.onClick.AddListener(() => Use(item));
        _discardButton.onClick.AddListener(() => Discard(item));
        _inspectButton.onClick.AddListener(() => Inspect(item));
        _cancelButton.onClick.AddListener(() => ClosePanel());

        _optionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, mousePosition, _parentCanvas.worldCamera, out Vector2 localMousePosition);
        _optionsPanel.transform.position = _parentCanvas.transform.TransformPoint(localMousePosition);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    public void OpenPanel(Document document)
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        _useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Read";

        _discardButton.gameObject.SetActive(false);

        _useButton.onClick.AddListener(() => Read(document));
        _inspectButton.onClick.AddListener(() => Inspect(document));
        _cancelButton.onClick.AddListener(() => ClosePanel());

        _optionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, mousePosition, _parentCanvas.worldCamera, out Vector2 localMousePosition);
        _optionsPanel.transform.position = _parentCanvas.transform.TransformPoint(localMousePosition);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    private void Use(Item item)
    {
        item.Use();
        ClosePanel();
    }
    private void Read(Document document)
    {
        document.Read();
        ClosePanel();
    }
    private void Inspect(Entity entity)
    {
        ObjectInspectorController.Instance.enabled = true;
        ObjectInspectorController.Instance.Initialize(entity);

        ClosePanel();
    }
    private void Discard(Item item)
    {
        Player.Inventory.Remove(item);
        ClosePanel();
    }
    private void ClosePanel()
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), UIController.Instance.gameObject.GetComponent<AudioSource>());

        _optionsPanel.SetActive(false);
    }
}
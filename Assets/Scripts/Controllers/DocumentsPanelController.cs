using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentsPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private GameObject _documentsPanel;
    [SerializeField] private Button _closeButton;

    public static DocumentsPanelController Instance;

    private void Awake()
    {
        Instance = this;
        _closeButton.onClick.AddListener(() => ClosePanel());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]) || Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.AlternativeCloseInventory]))
            ClosePanel();
    }

    private void ClosePanel()
    {
        _documentsPanel.SetActive(false);
        enabled = false;
    }

    public void Initialize(string text)
    {
        _documentsPanel.SetActive(true);
        _textUI.text = text;
    }
}
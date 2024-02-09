using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _inspectButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private Button _cancelButton;

    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (GameManager.TypeOfControl == TypesOfControl.InventoryControl)
        {
            if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]) || Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.AlternativeCloseInventory]))
                CloseInventory();
        }

        if (GameManager.TypeOfControl == TypesOfControl.InteractionControl && Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]))
            CloseInventory();
    }
    private void OnEnable()
    {
        _inventory.SetActive(true);
        Player.Inventory.OnSlotsChanged += UpdateInventory;
        UpdateInventory();
    }
    private void OnDisable()
    {
        _inventory.SetActive(false);
        Player.Inventory.OnSlotsChanged -= UpdateInventory;
    }

    private void OpenOptionsPanel(TypesOfEntity type)
    {
        _useButton.onClick.AddListener(() => UseItem(type));
        _inspectButton.onClick.AddListener(() => InspectItem(type));
        _discardButton.onClick.AddListener(() => DiscardItem(type));
        _cancelButton.onClick.AddListener(() => CloseOptionsPanel());

        _optionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, mousePosition, _parentCanvas.worldCamera, out Vector2 localMousePosition);
        _optionsPanel.transform.position = _parentCanvas.transform.TransformPoint(localMousePosition);
    }
    private void UseItem(TypesOfEntity type)
    {
        Item item = EntitiesList.Entities[type] as Item;
        item.Use();
        CloseOptionsPanel();
    }
    private void InspectItem(TypesOfEntity type)
    {
        InspectionController.Instance.enabled = true;
        InspectionController.Instance.Initialize(type);

        CloseOptionsPanel();
    }
    private void DiscardItem(TypesOfEntity type)
    {
        Item item = EntitiesList.Entities[type] as Item;
        Player.Inventory.Remove(item);
        CloseOptionsPanel();
    }
    private void UpdateInventory()
    {
        foreach (Transform itemSlot in _itemContainer)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (InventorySlot slot in Player.Inventory.Slots)
        {
            Item item = EntitiesList.Entities[slot.Type] as Item;
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlot"), _itemContainer, false);
            itemSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.Sprite;
            itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            itemSlot.GetComponent<Button>().onClick.AddListener(() => OpenOptionsPanel(slot.Type));
        }
    }
    private void CloseOptionsPanel()
    {
        _useButton.onClick.RemoveAllListeners();
        _inspectButton.onClick.RemoveAllListeners();
        _discardButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        _optionsPanel.SetActive(false);
    }
    private void CloseInventory()
    {
        CloseOptionsPanel();

        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.ResumeGame();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        enabled = false;
    }
}
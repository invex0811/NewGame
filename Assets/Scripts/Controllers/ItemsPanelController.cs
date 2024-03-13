using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _itemsPanel;
    [SerializeField] private Transform _itemContainer;

    public static ItemsPanelController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InventoryController.Instance.OnPanelChange += ClosePanel;
        _itemsPanel.SetActive(true);
        Player.Inventory.OnSlotsChanged += UpdateInventory;
        UpdateInventory();
    }
    private void OnDisable()
    {
        InventoryController.Instance.OnPanelChange -= ClosePanel;
        _itemsPanel.SetActive(false);
        Player.Inventory.OnSlotsChanged -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        foreach (Transform itemSlot in _itemContainer)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (Slot slot in Player.Inventory.Slots)
        {
            Item item = slot.Entity as Item;
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlot"), _itemContainer, false);
            itemSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.ScriptableObject.Sprite;
            itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.ScriptableObject.DisplayName;
            itemSlot.GetComponent<Button>().onClick.AddListener(() => OptionsPanelController.Instance.OpenPanel(slot.Entity));
        }
    }
    private void ClosePanel()
    {
        enabled = false;
    }
}
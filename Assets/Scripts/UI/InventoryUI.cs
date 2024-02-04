using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class InventoryUI : MonoBehaviour
{
    public GameObject Inventory;
    public Transform ItemContainer;

    private void OnEnable()
    {
        UpdateInventory();
    }
    private void Update()
    {
        if (GameManager.TypeOfControl != TypesOfControl.InventoryControl)
        {
            Debug.LogError("Type of controll conflict");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.Inventory.Add(ItemsList.Items[0]); // Код для дебага. Добавляет предмет "Key" в инвентарь игрока.
            UpdateInventory();
        }

        if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]))
            CloseInventory();
    }

    private void UpdateInventory()
    {
        foreach (Transform itemSlot in ItemContainer)
        {
            Destroy(itemSlot.gameObject);
        }
        
        foreach  (InventorySlot slot in Player.Inventory.Slots)
        {
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlot"), ItemContainer, false);
            itemSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = ItemsList.Items[slot.ItemID].Sprite;
            itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = ItemsList.Items[slot.ItemID].DisplayName;
            itemSlot.GetComponent<Button>().onClick.AddListener(() => UseItem(slot.ItemID));
        }
    }
    private void UseItem(int ID)
    {
        ItemsList.Items[ID].Use();
        UpdateInventory();
    }
    private void CloseInventory()
    {
        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.TogglePause();

        Inventory.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}
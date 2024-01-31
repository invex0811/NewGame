using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems; // ƒобавленна€ директива

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    public Image icon;
    public GameObject itemIconPrefab;
    public List<Sprite> itemIcons = new List<Sprite>();

    public void AddItem(Sprite itemIcon)
    {
        itemIcons.Add(itemIcon);

        GameObject itemIconObject = Instantiate(itemIconPrefab, transform);
        Image itemIconImage = itemIconObject.GetComponent<Image>();

        itemIconImage.sprite = itemIcon;
        itemIconImage.enabled = true;
    }

    public void ClearSlot()
    {
        itemIcons.Clear();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public bool IsEmpty()
    {
        return itemIcons.Count == 0;
    }

    public void MoveItemToSlot(InventorySlot targetSlot)
    {
        if (!targetSlot.IsEmpty())
        {
            Sprite tempIcon = targetSlot.itemIcons[0];
            targetSlot.ClearSlot();
            AddItem(tempIcon);
        }
    }

    public Sprite GetItemIcon()
    {
        if (itemIcons.Count > 0)
        {
            return itemIcons[0];
        }
        return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ”ведомл€ем InventoryManager, что указатель мыши находитс€ над этим слотом
        InventoryManager.instance.SetMouseOverSlot(this);
    }
}
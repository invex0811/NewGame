using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject draggedItem;
    private Vector3 offset;
    public GameObject slotPrefab;
    public Transform grid;
    private InventorySlot draggedItemSlot;
    private Image draggedItemIcon;
    private bool isDraggingItem => draggedItem != null;
    private InventorySlot mouseOverSlot;
    public static InventoryManager instance;
    private InventorySlot[] allSlots;  // Добавлено поле для хранения всех слотов

    private void Awake()
    {
        instance = this;
    }

    public void SetMouseOverSlot(InventorySlot slot)
    {
        mouseOverSlot = slot;
    }

    public void ClearMouseOverSlot()
    {
        mouseOverSlot = null;
    }

    void Start()
    {
        CreateSlots();
        // Получаем все слоты внутри grid
        allSlots = grid.GetComponentsInChildren<InventorySlot>();
    }

    public void AddItemToInventory(Sprite itemIcon)
    {
        // Получаем все слоты внутри grid
        InventorySlot[] slots = grid.GetComponentsInChildren<InventorySlot>();

        // Ищем первый пустой слот
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                // Если нашли пустой слот, добавим в него спрайт
                slot.AddItem(itemIcon);
                return;
            }
        }

        // Если дошли сюда, значит инвентарь полон
        Debug.Log("Inventory is full!");
        // Здесь можете добавить код для обработки случая, когда инвентарь полон
    }

    void CreateSlots()
    {
        int numberOfSlots = 4;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, grid);
            InventorySlot slotComponent = slotObject.GetComponent<InventorySlot>();

            if (slotComponent != null)
            {
                slotComponent.ClearSlot();
            }
            else
            {
                Debug.LogError("InventorySlot component not found on the created slot object.");
            }
        }
    }

    void Update()
    {
        if (isDraggingItem)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
            draggedItem.transform.position = newPosition;

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (mouseOverSlot != null)
                {
                    if (mouseOverSlot.IsEmpty())
                    {
                        draggedItemSlot.ClearSlot();
                        mouseOverSlot.AddItem(draggedItemIcon.sprite);
                    }
                    else
                    {
                        draggedItemSlot.MoveItemToSlot(mouseOverSlot);
                    }
                }

                ClearMouseOverSlot();
                StopDraggingItem();

                if (Physics.Raycast(ray, out hit))
                {
                    InventorySlot slot = hit.collider.GetComponent<InventorySlot>();

                    if (slot != null)
                    {
                        if (slot.IsEmpty())
                        {
                            draggedItemSlot.ClearSlot();
                            slot.AddItem(draggedItemIcon.sprite);
                        }
                        else
                        {
                            draggedItemSlot.MoveItemToSlot(slot);
                        }
                    }
                }

                StopDraggingItem();
            }
        }
    }


    public void StartDraggingItem(GameObject item)
    {
        Debug.Log("Start Dragging Item");

        draggedItem = item;
        draggedItemSlot = draggedItem.GetComponentInParent<InventorySlot>();
        offset = draggedItem.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggedItemIcon = draggedItemSlot.icon;
    }
    public void StopDraggingItem()
    {
        Debug.Log("Stop Dragging Item");

        draggedItem = null;
        draggedItemSlot = null;
        draggedItemIcon = null;
    }


    public InventorySlot FindClosestSlot(Vector3 position)
    {
        InventorySlot closestSlot = null;
        float closestDistance = float.MaxValue;

        foreach (InventorySlot slot in allSlots)
        {
            if (slot != null)
            {
                float distance = Vector3.Distance(position, slot.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSlot = slot;
                }
            }
        }

        return closestSlot;
    }

}

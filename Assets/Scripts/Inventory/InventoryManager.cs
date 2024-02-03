using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //private Image _draggedItemIcon;
    //private InventorySlot[] _Slots;
    //private InventorySlot _mouseOverSlot;
    //private InventorySlot _draggedItemSlot;
    //private Vector3 _offset;
    //private int _inventorySize = 4;
    //private bool _isDraggingItem => DraggedItem != null;

    //public static InventoryManager Instance;

    //public GameObject DraggedItem;
    //public GameObject SlotPrefab;
    //public Transform Grid;

    //private void Awake()
    //{
    //    Instance = this;
    //}
    //private void Start()
    //{
    //    CreateSlots();
    //    _Slots = Grid.GetComponentsInChildren<InventorySlot>();
    //}
    //private void CreateSlots()
    //{
    //    for (int i = 0; i < _inventorySize; i++)
    //    {
    //        GameObject slotObject = Instantiate(SlotPrefab, Grid);
    //        InventorySlot slotComponent = slotObject.GetComponent<InventorySlot>();

    //        if (slotComponent == null)
    //        {
    //            Debug.LogError("InventorySlot component not found on the created slot object.");
    //            return;
    //        }

    //        slotComponent.ClearSlot();
    //    }
    //}
    //private void Update()
    //{
    //    if (!_isDraggingItem) return;

    //    Vector3 mousePosition = Input.mousePosition;
    //    mousePosition.z = 10f;
    //    Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + _offset;
    //    DraggedItem.transform.position = newPosition;

    //    if (!Input.GetMouseButtonUp(0)) return;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (_mouseOverSlot == null) return;

    //    if (_mouseOverSlot.IsEmpty())
    //    {
    //        _draggedItemSlot.ClearSlot();
    //        _mouseOverSlot.AddItem(_draggedItemIcon.sprite);
    //    }
    //    else
    //    {
    //        _draggedItemSlot.MoveItemToSlot(_mouseOverSlot);
    //    }

    //    ClearMouseOverSlot();
    //    StopDraggingItem();

    //    if (!Physics.Raycast(ray, out hit)) return;

    //    InventorySlot slot = hit.collider.GetComponent<InventorySlot>();

    //    if (slot == null) return;

    //    if (slot.IsEmpty())
    //    {
    //        _draggedItemSlot.ClearSlot();
    //        slot.AddItem(_draggedItemIcon.sprite);
    //    }
    //    else
    //    {
    //        _draggedItemSlot.MoveItemToSlot(slot);
    //    }
    //    StopDraggingItem();
    //}
    //public void SetMouseOverSlot(InventorySlot slot)
    //{
    //    _mouseOverSlot = slot;
    //}
    //public void ClearMouseOverSlot()
    //{
    //    _mouseOverSlot = null;
    //}
    //public void AddItemToInventory(Sprite itemIcon)
    //{
    //    // Получаем все слоты внутри grid
    //    InventorySlot[] slots = Grid.GetComponentsInChildren<InventorySlot>();

    //    // Ищем первый пустой слот
    //    foreach (InventorySlot slot in slots)
    //    {
    //        if (slot.IsEmpty())
    //        {
    //            // Если нашли пустой слот, добавим в него спрайт
    //            slot.AddItem(itemIcon);
    //            return;
    //        }
    //    }

    //    // Если дошли сюда, значит инвентарь полон
    //    Debug.Log("Inventory is full!");
    //    // Здесь можете добавить код для обработки случая, когда инвентарь полон
    //}
    //public void StartDraggingItem(GameObject item)
    //{
    //    Debug.Log("Start Dragging Item");

    //    DraggedItem = item;
    //    _draggedItemSlot = DraggedItem.GetComponentInParent<InventorySlot>();
    //    _offset = DraggedItem.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    _draggedItemIcon = _draggedItemSlot.icon;
    //}
    //public void StopDraggingItem()
    //{
    //    Debug.Log("Stop Dragging Item");

    //    DraggedItem = null;
    //    _draggedItemSlot = null;
    //    _draggedItemIcon = null;
    //}
    //public InventorySlot FindClosestSlot(Vector3 position)
    //{
    //    InventorySlot closestSlot = null;
    //    float closestDistance = float.MaxValue;

    //    foreach (InventorySlot slot in _Slots)
    //    {
    //        if (slot != null)
    //        {
    //            float distance = Vector3.Distance(position, slot.transform.position);

    //            if (distance < closestDistance)
    //            {
    //                closestDistance = distance;
    //                closestSlot = slot;
    //            }
    //        }
    //    }

    //    return closestSlot;
    //}
}

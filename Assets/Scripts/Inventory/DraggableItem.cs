using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour/*, IPointerDownHandler, IDragHandler, IPointerUpHandler*/
{
    //private bool isDragging = false;
    //private Vector2 offset;
    //private InventorySlot currentSlot;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        isDragging = true;

    //        // Получаем точку попадания рейкаста в координатах Canvas
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out offset);

    //        // Определяем текущий слот, если иконка начинает перетаскиваться
    //        currentSlot = transform.parent.GetComponent<InventorySlot>();

    //        // Отвязываем иконку от слота
    //        transform.SetParent(transform.parent.parent);  // Родителем становится Canvas
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (isDragging)
    //    {
    //        // Обновляем локальную позицию иконки в координатах курсора
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 newPosition);
    //        transform.localPosition = newPosition - offset;
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (isDragging)
    //    {
    //        isDragging = false;

    //        // Проверяем, что текущий слот был инициализирован
    //        if (currentSlot != null)
    //        {
    //            // Получаем объект, над которым находится указатель мыши
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                // Проверяем, является ли объект слотом
    //                InventorySlot slot = hit.collider.GetComponent<InventorySlot>();

    //                if (slot != null)
    //                {
    //                    // Если слот не пустой, перемещаем предмет
    //                    if (!slot.IsEmpty())
    //                    {
    //                        // Перемещаем предмет между слотами
    //                        InventorySlot startSlot = currentSlot;
    //                        InventorySlot endSlot = slot;

    //                        // Если слоты различны
    //                        if (startSlot != endSlot)
    //                        {
    //                            // Получаем иконку из начального слота
    //                            Sprite startIcon = startSlot.GetItemIcon();

    //                            // Очищаем начальный слот
    //                            startSlot.ClearSlot();

    //                            // Устанавливаем иконку в новый слот и делаем ее дочерним объектом слота
    //                            endSlot.AddItem(startIcon);
    //                            transform.SetParent(endSlot.transform);
    //                            transform.localPosition = Vector3.zero;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        // Если слот пустой, просто устанавливаем иконку в новый слот
    //                        slot.AddItem(currentSlot.GetItemIcon());
    //                        transform.SetParent(slot.transform);
    //                        transform.localPosition = Vector3.zero;
    //                    }
    //                }
    //                else
    //                {
    //                    // Если не попали в слот, просто возвращаем иконку на место
    //                    transform.SetParent(currentSlot.transform);
    //                    transform.localPosition = Vector3.zero;
    //                }
    //            }
    //        }
    //    }
    //}
}
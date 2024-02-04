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

    //        // �������� ����� ��������� �������� � ����������� Canvas
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out offset);

    //        // ���������� ������� ����, ���� ������ �������� ���������������
    //        currentSlot = transform.parent.GetComponent<InventorySlot>();

    //        // ���������� ������ �� �����
    //        transform.SetParent(transform.parent.parent);  // ��������� ���������� Canvas
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (isDragging)
    //    {
    //        // ��������� ��������� ������� ������ � ����������� �������
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 newPosition);
    //        transform.localPosition = newPosition - offset;
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (isDragging)
    //    {
    //        isDragging = false;

    //        // ���������, ��� ������� ���� ��� ���������������
    //        if (currentSlot != null)
    //        {
    //            // �������� ������, ��� ������� ��������� ��������� ����
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                // ���������, �������� �� ������ ������
    //                InventorySlot slot = hit.collider.GetComponent<InventorySlot>();

    //                if (slot != null)
    //                {
    //                    // ���� ���� �� ������, ���������� �������
    //                    if (!slot.IsEmpty())
    //                    {
    //                        // ���������� ������� ����� �������
    //                        InventorySlot startSlot = currentSlot;
    //                        InventorySlot endSlot = slot;

    //                        // ���� ����� ��������
    //                        if (startSlot != endSlot)
    //                        {
    //                            // �������� ������ �� ���������� �����
    //                            Sprite startIcon = startSlot.GetItemIcon();

    //                            // ������� ��������� ����
    //                            startSlot.ClearSlot();

    //                            // ������������� ������ � ����� ���� � ������ �� �������� �������� �����
    //                            endSlot.AddItem(startIcon);
    //                            transform.SetParent(endSlot.transform);
    //                            transform.localPosition = Vector3.zero;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        // ���� ���� ������, ������ ������������� ������ � ����� ����
    //                        slot.AddItem(currentSlot.GetItemIcon());
    //                        transform.SetParent(slot.transform);
    //                        transform.localPosition = Vector3.zero;
    //                    }
    //                }
    //                else
    //                {
    //                    // ���� �� ������ � ����, ������ ���������� ������ �� �����
    //                    transform.SetParent(currentSlot.transform);
    //                    transform.localPosition = Vector3.zero;
    //                }
    //            }
    //        }
    //    }
    //}
}
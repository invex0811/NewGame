using System.Collections;
using UnityEngine;
using System.Linq;

public class ItemTemp : MonoBehaviour
{
    //public float interactionRadius = 7f;
    //public GameObject itemInventory;
    //public Transform grid;
    //public GameObject slotPrefab;
    //public Sprite itemIcon;

    //private bool isAddedToInventory = false;
    //private Vector3 spritePositionOffset = new Vector3(0f, 1f, 0f);
    //private Vector3 spriteRotation = new Vector3(0f, 180f, 0f);
    //private bool isSpriteSpawned = false;
    //private GameObject currentIndicatorSpriteObject;
    //public GameObject indicatorSpritePrefab;
    //private bool isPaused = false;
    //private InventoryManager inventoryManager;

    //private void Start()
    //{
    //    inventoryManager = Object.FindAnyObjectByType<InventoryManager>();
    //}

    //private void Update()
    //{
    //    float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

    //    if (distanceToPlayer <= interactionRadius)
    //    {
    //        ShowSprite(transform.position);

    //        if (Input.GetKeyDown(KeyCode.E) && !isAddedToInventory)
    //        {
    //            if (TryAddToInventory())
    //            {
    //                Destroy(gameObject);
    //                Destroy(currentIndicatorSpriteObject);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        HideSprite();
    //    }
    //}

    //private IEnumerator HideIndicatorAfterDelay()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    Debug.Log("Indicator hidden!");
    //}

    //void ShowSprite(Vector3 position)
    //{
    //    if (indicatorSpritePrefab != null && !isSpriteSpawned)
    //    {
    //        currentIndicatorSpriteObject = Instantiate(indicatorSpritePrefab, position + spritePositionOffset, Quaternion.Euler(spriteRotation));
    //        currentIndicatorSpriteObject.tag = "IndicatorSprite";
    //        currentIndicatorSpriteObject.SetActive(!isPaused);
    //        isSpriteSpawned = true;
    //    }
    //    else if (indicatorSpritePrefab == null)
    //    {
    //        Debug.LogError("Префаб индикатора спрайта не назначен в инспекторе!");
    //    }
    //    else
    //    {
    //        currentIndicatorSpriteObject.transform.position = position + spritePositionOffset;
    //        currentIndicatorSpriteObject.SetActive(!isPaused);
    //    }
    //}

    //void HideSprite()
    //{
    //    if (currentIndicatorSpriteObject != null)
    //    {
    //        currentIndicatorSpriteObject.SetActive(false);
    //    }
    //}

    //bool TryAddToInventory()
    //{
    //    // Получаем все пустые слоты в инвентаре
    //    InventorySlot[] emptySlots = grid.GetComponentsInChildren<InventorySlot>().Where(slot => slot.IsEmpty()).ToArray();

    //    if (emptySlots.Length > 0)
    //    {
    //        // Если есть хотя бы один свободный слот, добавляем в него предмет
    //        InventorySlot firstEmptySlot = emptySlots[0];

    //        if (itemIcon == null)
    //        {
    //            Debug.LogError("Иконка предмета не назначена!");
    //            return false;
    //        }

    //        firstEmptySlot.AddItem(itemIcon);
    //        isAddedToInventory = true;
    //        return true;
    //    }
    //    else
    //    {
    //        // Если инвентарь заполнен, возвращаем false
    //        return false;
    //    }
    //}
}

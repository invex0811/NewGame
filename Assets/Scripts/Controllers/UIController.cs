using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject Inventory;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.Inventory.Add(EntitiesList.Entities[0] as Item); // Код для дебага. Добавляет предмет "Key" в инвентарь игрока.
            InventoryController.Instance.UpdateInventory();
        }
    }

    public void OpenInventory()
    {
        Inventory.SetActive(true);
    }
}
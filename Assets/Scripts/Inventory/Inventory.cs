using System.Collections.Generic;
using UnityEngine;

class Inventory
{
    public List<InventorySlot> Slots { get; private set; } = new List<InventorySlot>();

    public void Add(Item item)
    {
        if (Slots.Count >= 30)
            return;

        InventorySlot newSlot = new(ItemsList.GetID(item));
        Slots.Add(newSlot);
    }

    public void Remove(Item item)
    {
        int index = Slots.IndexOf(Slots.Find(s => s.ItemID == ItemsList.GetID(item)));

        if (Slots[index] == null) return;

        Slots.Remove(Slots[index]);
    }

    //public GameObject InventoryObject;
    //public bool Flag = false;

    //void Start()
    //{
    //    Cursor.visible = false;
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
    //}
    //public void ToggleInventory()
    //{
    //    Flag = !Flag;
    //    InventoryObject.SetActive(Flag);
    //    Cursor.visible = Flag;

    //    if (Flag)
    //    {
    //        Cursor.lockState = CursorLockMode.Confined;
    //        Time.timeScale = 0f;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Time.timeScale = 1f;
    //    }

    //    CameraController cameraController = CameraController.instance;

    //    if (cameraController == null) return;

    //    if (Flag)
    //    {
    //        cameraController.DisableRotation();
    //    }
    //    else
    //    {
    //        cameraController.EnableRotation();
    //    }
}
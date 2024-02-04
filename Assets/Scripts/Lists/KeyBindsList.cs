using System;
using System.Collections.Generic;
using UnityEngine;
static class KeyBindsList
{
    public static readonly Dictionary<PlayerControllBindTypes, KeyCode> PlayerControllBinds = new()
    {
        {PlayerControllBindTypes.StopInteraction, KeyCode.Escape},
        {PlayerControllBindTypes.OpenInventory, KeyCode.I},
        {PlayerControllBindTypes.Interact, KeyCode.E},
        {PlayerControllBindTypes.Crouch, KeyCode.LeftControl}
    };
    public static readonly Dictionary<InventoryControllBindTypes, KeyCode> InventoryControllBinds = new()
    {
        {InventoryControllBindTypes.CloseInventory, KeyCode.Escape},
    };
}

enum PlayerControllBindTypes
{
    StopInteraction,
    OpenInventory,
    Interact,
    Crouch
}
enum InventoryControllBindTypes
{
    CloseInventory
}
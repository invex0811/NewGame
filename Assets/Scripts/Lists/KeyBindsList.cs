using System.Collections.Generic;
using UnityEngine;
static class KeyBindsList
{
    public static readonly Dictionary<PlayerControllBindTypes, KeyCode> PlayerControllBinds = new()
    {
        {PlayerControllBindTypes.StopInteraction, KeyCode.Escape},
        {PlayerControllBindTypes.OpenInventory, KeyCode.I},
        {PlayerControllBindTypes.Interact, KeyCode.E},
        {PlayerControllBindTypes.ToogleFlashlight, KeyCode.F},
        {PlayerControllBindTypes.Sprint, KeyCode.LeftShift}
    };
    public static readonly Dictionary<InventoryControllBindTypes, KeyCode> InventoryControllBinds = new()
    {
        {InventoryControllBindTypes.CloseInventory, KeyCode.Escape},
        {InventoryControllBindTypes.AlternativeCloseInventory, KeyCode.I},
        {InventoryControllBindTypes.ResetInspectionPanel, KeyCode.R},
    };
    public static readonly Dictionary<InteractionControllBindTypes, KeyCode> InteractionControllBinds = new()
    {
        {InteractionControllBindTypes.StopInteraction, KeyCode.Escape},
    };
}

enum PlayerControllBindTypes
{
    StopInteraction,
    OpenInventory,
    Interact,
    ToogleFlashlight,
    Sprint
}
enum InventoryControllBindTypes
{
    CloseInventory,
    AlternativeCloseInventory,
    ResetInspectionPanel
}
enum InteractionControllBindTypes
{
    StopInteraction
}
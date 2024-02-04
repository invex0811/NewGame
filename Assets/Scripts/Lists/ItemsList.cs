using System.Collections.Generic;
using UnityEngine;

static class ItemsList
{
    public static readonly List<Item> Items = new()
    {
        new Key(
            "Key",
            "Opens doors",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/ItemSlot")
            ),
        new VideoTape(
            "VideoTape",
            "Plays video if inserted into TV",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/ItemSlot")
            )
    };
    public static int GetID(Item item)
    {
        return Items.IndexOf(item);
    }
}
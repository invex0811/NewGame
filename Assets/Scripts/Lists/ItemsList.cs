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
            Resources.Load<GameObject>("Prefabs/Key3DObjectPrafab")
            ),
        new VideoTape(
            "Video Tape",
            "Plays video if inserted into TV",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/VideoTape/VideoTape")
            ),
        new Note(
            "Note",
            "Can be read",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/Note")
            )
    };
    public static int GetID(Item item)
    {
        return Items.IndexOf(item);
    }
}
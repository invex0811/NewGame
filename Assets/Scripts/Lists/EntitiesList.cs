using System.Collections.Generic;
using UnityEngine;

static class EntitiesList
{
    public static readonly List<Entity> Entities = new()
    {
        new Key(
            0,
            "Key",
            "Opens doors",
            "Pickup",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/Key3DObjectPrafab")
            ),
        new VideoTape(
            1,
            "Video Tape",
            "Plays video if inserted into TV",
            "Pickup",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/VideoTape/VideoTape")
            ),
        new Note(
            2,
            "Note",
            "Can be read",
            "Pickup",
            Resources.Load<Sprite>("Sprites/E"),
            Resources.Load<GameObject>("Prefabs/Note")
            ),
        new Painting(
            3,
            "Painting",
            "Work of fart",
            "Inspect",
            Resources.Load<GameObject>("Prefabs/Painting")
            ),
        new TV(
            4,
            "TV",
            "",
            "Inspect",
            null
            ),
        new Door(
            5,
            "Door",
            "",
            "Interact",
            null
            )
    };

    public static int GetID(Entity entity)
    {
        return Entities.IndexOf(entity);
    }
}
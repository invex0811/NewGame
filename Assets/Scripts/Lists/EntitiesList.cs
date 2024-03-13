using System.Collections.Generic;
using UnityEngine;

static class EntitiesList
{
    public static readonly Dictionary<EntityType, Entity> Entities = new()
    {
        {
            EntityType.Key, new Key
            (
                EntityType.Key,
                "Key",
                "Opens doors",
                "Pickup",
                Resources.Load<Sprite>("Sprites/E"),
                Resources.Load<GameObject>("Prefabs/Key3DObjectPrafab")
            )
        },
        {
            EntityType.VideoTape, new VideoTape
            (
                EntityType.VideoTape,
                "Video Tape",
                "Plays video if inserted into TV",
                "Pickup",
                Resources.Load<Sprite>("Sprites/E"),
                Resources.Load<GameObject>("Prefabs/VideoTape")
            )
        },
        {
            EntityType.Note, new Note
            (
                EntityType.Note,
                "Note",
                "Can be read",
                "Pickup",
                Resources.Load<GameObject>("Prefabs/Note"),
                "Padlock - rqt \r\n Password - 1234"
            )
        },
        {
            EntityType.Painting, new Painting
            (
                EntityType.Painting,
                "Painting",
                "Work of fart",
                "Inspect",
                Resources.Load<GameObject>("Prefabs/Painting")
            )
        },
        {
            EntityType.TV, new TV
            (
                EntityType.TV,
                "TV",
                "",
                "Inspect",
                null
            )
        },
        {
            EntityType.Door, new Door
            (
                EntityType.Door,
                "Door",
                "",
                "Interact",
                null
            )
        },
        {
            EntityType.SafeDigital, new SafeDigital
            (
                EntityType.SafeDigital,
                "Safe",
                "",
                "Inspect",
                null
            )
        },
        {
            EntityType.SafePadlock, new SafePadlock
            (
                EntityType.SafePadlock,
                "Safe",
                "",
                "Inspect",
                null
            )
        }
    };
}

public enum EntityType
{
    Key,
    VideoTape,
    Note,
    Painting,
    TV,
    Door,
    SafeDigital,
    SafePadlock,
}
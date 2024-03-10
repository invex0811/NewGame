using System.Collections.Generic;
using UnityEngine;

static class EntitiesList
{
    public static readonly Dictionary<TypesOfEntity, Entity> Entities = new()
    {
        {
            TypesOfEntity.Key, new Key
            (
                TypesOfEntity.Key,
                "Key",
                "Opens doors",
                "Pickup",
                Resources.Load<Sprite>("Sprites/E"),
                Resources.Load<GameObject>("Prefabs/Key3DObjectPrafab")
            )
        },
        {
            TypesOfEntity.VideoTape, new VideoTape
            (
                TypesOfEntity.VideoTape,
                "Video Tape",
                "Plays video if inserted into TV",
                "Pickup",
                Resources.Load<Sprite>("Sprites/E"),
                Resources.Load<GameObject>("Prefabs/VideoTape")
            )
        },
        {
            TypesOfEntity.Note, new Note
            (
                TypesOfEntity.Note,
                "Note",
                "Can be read",
                "Pickup",
                Resources.Load<GameObject>("Prefabs/Note"),
                "Це все хуйня, це все до сраки -\r\nОці мімози, рози, маки,\r\nОце кохання без їбання,\r\nОце їбання без кохання\r\nЦе все хуйня,\r\nЛиш тільки ти,\r\nЛиш тільки я,\r\nТа ні, і ти хуйня,\r\nЛиш тільки я !\r\n\r\nЦе все хуйня, це все до сраки,\r\nі ті червоні в полі маки,\r\nі та любовь, шо за селом-\r\nце все хуйня! Лишь тільки ти да я!\r\nИ зорі ті,що в небі сяють-\r\nце теж хуйня, це все до сраки!\r\nЛиш тільки ти да я!..\r\nДа і ти-хуйня! Лиш тільки Я!.."
            )
        },
        {
            TypesOfEntity.Painting, new Painting
            (
                TypesOfEntity.Painting,
                "Painting",
                "Work of fart",
                "Inspect",
                Resources.Load<GameObject>("Prefabs/Painting")
            )
        },
        {
            TypesOfEntity.TV, new TV
            (
                TypesOfEntity.TV,
                "TV",
                "",
                "Inspect",
                null
            )
        },
        {
            TypesOfEntity.Door, new Door
            (
                TypesOfEntity.Door,
                "Door",
                "",
                "Interact",
                null
            )
        },
        {
            TypesOfEntity.SafeDigital, new SafeDigital
            (
                TypesOfEntity.SafeDigital,
                "Safe",
                "",
                "Inspect",
                null
            )
        },
        {
            TypesOfEntity.SafePadlock, new SafePadlock
            (
                TypesOfEntity.SafePadlock,
                "Safe",
                "",
                "Inspect",
                null
            )
        }
    };
}

public enum TypesOfEntity
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
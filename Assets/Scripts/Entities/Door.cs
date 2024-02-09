using UnityEngine;

class Door : Entity
{
    private readonly int _id;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;

    public override int ID => _id;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;

    public Door(int id, string displayName, string description, string raycastFeedbackText, GameObject prefab)
    {
        _id = id;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override void Interact(GameObject obj)
    {
        DoorController door = obj.GetComponent<DoorController>();

        if (door.IsOpen)
            door.CloseDoor();
        else
            door.OpenDoor();
    }

    public override void StopInteraction()
    {
        throw new System.NotImplementedException();
    }
}
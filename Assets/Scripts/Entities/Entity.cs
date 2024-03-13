using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityScriptableObject _scriptableObject;
    public EntityScriptableObject ScriptableObject
    {
        get { return _scriptableObject; }
    }

    public void Interact()
    {
        Transform interactionPoint;

        switch (_scriptableObject.Type)
        {
            case EntityType.Key:
                Player.Inventory.Add(this as Item);
                Destroy(gameObject);

                break;
            case EntityType.VideoTape:
                Player.Inventory.Add(this as Item);
                Destroy(gameObject);

                break;
            case EntityType.Note:
                Player.Journal.Add(this as Document);

                Destroy(gameObject);

                break;
            case EntityType.Painting:
                GameManager.ChangeTypeOfControll(TypesOfControl.InspectionControll);
                ObjectInspectorController.Instance.enabled = true;
                ObjectInspectorController.Instance.Initialize(this);

                break;
            case EntityType.TV:
                GameManager.PauseGame();

                interactionPoint = InteractionPoints.Instance.TV.transform;
                InteractionController.Instance.InteractionPoint = interactionPoint;
                InteractionController.Instance.CurrentInteraction = InteractionType.TV;
                InteractionController.Instance.MoveToInteractionPoint();

                InteractionController.Instance.OnStopInteraction += StopInteraction;

                UIController.Instance.OpenInventory();
                PlayerController.Instance.enabled = false;

                break;
            case EntityType.Door:
                DoorController door = gameObject.GetComponent<DoorController>();

                if (door.IsOpen)
                    door.CloseDoor();
                else
                    door.OpenDoor();

                break;
            case EntityType.SafeDigital:
                GameManager.PauseGame();
                GameManager.ChangeTypeOfControll(TypesOfControl.InteractionControl);

                interactionPoint = InteractionPoints.Instance.SafeDigital.transform;
                InteractionController.Instance.InteractionPoint = interactionPoint;
                InteractionController.Instance.CurrentInteraction = InteractionType.SafeNumeric;
                InteractionController.Instance.MoveToInteractionPoint();

                InteractionController.Instance.OnStopInteraction += StopInteraction;

                PlayerController.Instance.enabled = false;
                CameraController.Instance.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                gameObject.GetComponentInChildren<SafeDoor>().enabled = true;

                break;
            case EntityType.SafePadlock:
                GameManager.PauseGame();
                GameManager.ChangeTypeOfControll(TypesOfControl.InteractionControl);

                interactionPoint = InteractionPoints.Instance.SafePadlock.transform;
                InteractionController.Instance.InteractionPoint = interactionPoint;
                InteractionController.Instance.CurrentInteraction = InteractionType.SafePadlock;
                InteractionController.Instance.MoveToInteractionPoint();

                InteractionController.Instance.OnStopInteraction += StopInteraction;

                PlayerController.Instance.enabled = false;
                CameraController.Instance.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                gameObject.GetComponentInChildren<Padlock>().enabled = true;

                break;
            default:
                break;
        }
    }
    public void StopInteraction()
    {
        switch (_scriptableObject.Type)
        {
            case EntityType.Key:
                break;
            case EntityType.VideoTape:
                break;
            case EntityType.Note:
                break;
            case EntityType.Painting:
                break;
            case EntityType.TV:
                InteractionController.Instance.OnStopInteraction -= StopInteraction;

                InteractionController.Instance.ReturnToInteractionPosition();
                InteractionController.Instance.CurrentInteraction = InteractionType.None;

                GameManager.ResumeGame();

                break;
            case EntityType.Door:
                break;
            case EntityType.SafeDigital:
                gameObject.GetComponentInChildren<SafeDoor>().enabled = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PlayerController.Instance.enabled = true;
                CameraController.Instance.enabled = true;

                InteractionController.Instance.OnStopInteraction -= StopInteraction;

                InteractionController.Instance.CurrentInteraction = InteractionType.None;
                InteractionController.Instance.ReturnToInteractionPosition();

                GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
                GameManager.ResumeGame();

                break;
            case EntityType.SafePadlock:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PlayerController.Instance.enabled = true;
                CameraController.Instance.enabled = true;

                InteractionController.Instance.OnStopInteraction -= StopInteraction;

                InteractionController.Instance.CurrentInteraction = InteractionType.None;
                InteractionController.Instance.ReturnToInteractionPosition();

                GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
                GameManager.ResumeGame();

                break;
            default:
                break;
        }
    }
}
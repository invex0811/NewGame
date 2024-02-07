using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hoverText;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private InteractionType _currentInteraction;
    [SerializeField] private float _interactionDistance = 10f;

    private Transform _interactionPoint;

    public static InteractionController Instance;

    public Transform InteractionPoint
    {
        set => _interactionPoint = value;
    }
    public InteractionType CurrentInteraction
    {
        get => _currentInteraction;
        set => _currentInteraction = value;
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(GameManager.TypeOfControl == TypesOfControl.PlayerControl)
        {
            InteractionRayCast();
            return;
        }

        if (CurrentInteraction != InteractionType.None && GameManager.TypeOfControl == TypesOfControl.InteractionControl && Input.GetKeyDown(KeyBindsList.InteractionControllBinds[InteractionControllBindTypes.StopInteraction]))
        {
            ReturnToInteractionPosition();
            PlayerController.Instance.enabled = true;
            CurrentInteraction = InteractionType.None;
            return;
        }
    }

    private void ReturnToInteractionPosition()
    {
        Transform initialPoint = InteractionPoints.Instance.InitialCameraPosition.transform;
        Camera.main.transform.SetLocalPositionAndRotation(initialPoint.localPosition, initialPoint.localRotation);
    }
    private void InteractionRayCast()
    {
        _hoverText.text = "";

        Ray ray = Camera.main.ScreenPointToRay(_hoverText.transform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableLayer))
        {
            if (hit.collider == null)
                return;

            int entityID = hit.collider.GetComponent<EntityID>().ID;

            if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Interact]))
            {
                EntitiesList.Entities[entityID].Interact(hit.collider.gameObject);

                return;
            }

            _hoverText.text = EntitiesList.Entities[entityID].RaycastFeedbackText;
        }
    }

    public void InspectEntity(int entityID)
    {
        GameManager.ChangeTypeOfControll(TypesOfControl.InspectionControll);
        PlayerController.Instance.enabled = false;

        InspectionController.Instance.enabled = true;
        OnInspect?.Invoke(entityID);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void MoveToInteractionPoint()
    {
        Camera.main.transform.SetPositionAndRotation(_interactionPoint.position, _interactionPoint.rotation);
    }

    public delegate void Action(int entityID);
    public event Action OnInspect;
}

public enum InteractionType
{
    None,
    TV
}
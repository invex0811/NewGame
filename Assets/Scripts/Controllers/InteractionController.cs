using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hoverText;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private InteractionType _currentInteraction;
    [SerializeField] private float _interactionDistance = 10f;

    private Transform _interactionPoint;
    private Vector3 _initialCameraPosition;
    private Quaternion _initialCameraRotation;

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
            OnStopInteraction?.Invoke();
            return;
        }
    }

    private void InteractionRayCast()
    {
        _hoverText.text = "";

        Ray ray = Camera.main.ScreenPointToRay(_hoverText.transform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableLayer))
        {
            if (hit.collider == null)
                return;

            EntityType type = hit.collider.GetComponent<EntityID>().Type;

            if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Interact]))
            {
                EntitiesList.Entities[type].Interact(hit.collider.gameObject);

                return;
            }

            _hoverText.text = EntitiesList.Entities[type].RaycastFeedbackText;
        }
    }

    public void MoveToInteractionPoint()
    {
        _initialCameraPosition = Camera.main.transform.localPosition;
        _initialCameraRotation = Camera.main.transform.localRotation;
        Camera.main.transform.SetPositionAndRotation(_interactionPoint.position, _interactionPoint.rotation);
    }
    public void ReturnToInteractionPosition()
    {
        Camera.main.transform.SetLocalPositionAndRotation(_initialCameraPosition, _initialCameraRotation);
    }

    public delegate void StopInteractionEventHandler();
    public event StopInteractionEventHandler OnStopInteraction;
}

public enum InteractionType
{
    None,
    TV,
    SafeNumeric,
    SafePadlock
}
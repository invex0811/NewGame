using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InteractionController : MonoBehaviour
{
    private Transform _interactionPoint;
    private InteractionType _currentInteraction;

    public static InteractionController Instance;

    public Camera MainCamera;
    public Camera InteractionCamera;
    public LayerMask ItemLayer;
    public LayerMask InteractableLayer;
    public TextMeshProUGUI HoverText;
    public float InteractionDistance = 10f;

    public InteractionType CurrentInteraction { get => _currentInteraction; }

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

        if (_currentInteraction != InteractionType.None && GameManager.TypeOfControl == TypesOfControl.InteractionControl && Input.GetKeyDown(KeyBindsList.InteractionControllBinds[InteractionControllBindTypes.StopInteraction]))
        {
            GameManager.SwitchCamera(MainCamera);
            ReturnToInteractionPosition();
            PlayerController.Instance.enabled = true;
            _currentInteraction = InteractionType.None;
            UIController.Instance.CloseInventory();
            return;
        }
    }

    private void MoveToInteractionPoint()
    {
        ToogleIntercationCamera();

        InteractionCamera.transform.position = _interactionPoint.position;
        InteractionCamera.transform.rotation = _interactionPoint.rotation;
    }
    private void ReturnToInteractionPosition()
    {
        ToogleIntercationCamera();
    }
    private void ToogleIntercationCamera()
    {
        if (MainCamera == null || InteractionCamera == null) return;
        InteractionCamera.enabled = !InteractionCamera.enabled;
        MainCamera.enabled = !MainCamera.enabled;
    }
    private void InteractionRayCast()
    {
        HoverText.text = "";

        if (GameManager.CurrentCamera != MainCamera)
            return;

        Ray ray = MainCamera.ScreenPointToRay(HoverText.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance, ItemLayer))
        {
            if (hit.collider == null)
                return;

            int itemID = hit.collider.GetComponent<ItemID>().ID;

            if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Interact]))
            {
                Player.Inventory.Add(ItemsList.Items[itemID]);
                Destroy(hit.collider.gameObject);
                return;
            }

            HoverText.text = "Pickup"; // Заменить на ItemsList.Items[itemID].DisplayName если нужно отображать название предмета
        }

        if (Physics.Raycast(ray, out hit, InteractionDistance, InteractableLayer))
        {
            if (hit.collider == null)
                return;

            if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Interact]))
            {
                _interactionPoint = hit.collider.GetComponent<InteractableObject>().InteractionPoint;
                _currentInteraction = hit.collider.GetComponent<InteractableObject>().InteractionType;

                PlayerController.Instance.OpenInventory();
                PlayerController.Instance.enabled = false;
                GameManager.SwitchCamera(InteractionCamera);
                MoveToInteractionPoint();

                return;
            }

            HoverText.text = "Press E";
        }
    }
}
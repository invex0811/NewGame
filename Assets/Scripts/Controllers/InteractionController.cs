using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{

    public static InteractionController Instance;

    public GameObject InspectionPanel;
    public Camera MainCamera;
    public Camera InteractionCamera;
    public Transform InteractionPoint;
    public TextMeshProUGUI HoverText;
    public LayerMask InteractableLayer;
    public InteractionType CurrentInteraction;
    public float InteractionDistance = 10f;

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
            GameManager.SwitchCamera(MainCamera);
            ReturnToInteractionPosition();
            PlayerController.Instance.enabled = true;
            CurrentInteraction = InteractionType.None;
            InventoryController.Instance.CloseInventory();
            return;
        }
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

        if (Physics.Raycast(ray, out RaycastHit hit, InteractionDistance, InteractableLayer))
        {
            if (hit.collider == null)
                return;

            int entityID = hit.collider.GetComponent<EntityID>().ID;

            if (Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.Interact]))
            {
                if (EntitiesList.Entities[entityID].Interact())
                    Destroy(hit.collider.gameObject);

                return;
            }

            HoverText.text = EntitiesList.Entities[entityID].RaycastFeedbackText;
        }
    }

    public void InspectEntity(int entityID)
    {
        GameManager.ChangeTypeOfControll(TypesOfControl.InspectionControll);
        PlayerController.Instance.enabled = false;

        InspectionPanel.SetActive(true);
        InspectionController.Instance.Initialize(entityID);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void MoveToInteractionPoint()
    {
        GameManager.SwitchCamera(InteractionCamera);
        ToogleIntercationCamera();

        InteractionCamera.transform.SetPositionAndRotation(InteractionPoint.position, InteractionPoint.rotation);
    }
}

public enum InteractionType
{
    None,
    TV
}
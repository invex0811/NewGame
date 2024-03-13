using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Button _itemsButton;
    [SerializeField] private Button _documentsButton;

    public static InventoryController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (GameManager.TypeOfControl == TypesOfControl.InventoryControl)
        {
            if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]) || Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.AlternativeCloseInventory]))
                CloseInventory();
        }

        if (GameManager.TypeOfControl == TypesOfControl.InteractionControl && Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]))
            CloseInventory();
    }
    private void OnEnable()
    {
        _inventory.SetActive(true);
        _itemsButton.onClick.AddListener(() => OpenItemsPanel());
        _documentsButton.onClick.AddListener(() => OpenDocumentsPanel());
        OptionsPanelController.Instance.enabled = true;
        OpenItemsPanel();
    }
    private void OnDisable()
    {
        OptionsPanelController.Instance.enabled = false;
        _itemsButton.onClick.RemoveAllListeners();
        _documentsButton.onClick.RemoveAllListeners();
        _inventory.SetActive(false);
    }
    private void CloseInventory()
    {
        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;
        OptionsPanelController.Instance.enabled = false;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.ResumeGame();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        enabled = false;
    }
    private void OpenItemsPanel()
    {
        OnPanelChange?.Invoke();
        ItemsPanelController.Instance.enabled = true;
        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }
    private void OpenDocumentsPanel()
    {
        OnPanelChange?.Invoke();
        JournalPanelController.Instance.enabled = true;
        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.ButtonClick], UIController.Instance.gameObject.GetComponent<AudioSource>());
    }

    public event Action OnPanelChange;
}
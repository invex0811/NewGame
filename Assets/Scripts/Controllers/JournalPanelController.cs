using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _journalPanel;
    [SerializeField] private Transform _documentContainer;

    public static JournalPanelController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InventoryController.Instance.OnPanelChange += ClosePanel;
        _journalPanel.SetActive(true);
        Player.Journal.OnSlotsChanged += UpdateJournal;
        UpdateJournal();
    }
    private void OnDisable()
    {
        InventoryController.Instance.OnPanelChange -= ClosePanel;
        _journalPanel.SetActive(false);
        Player.Journal.OnSlotsChanged -= UpdateJournal;
    }

    private void UpdateJournal()
    {
        foreach (Transform document in _documentContainer.transform)
        {
            Destroy(document.gameObject);
        }

        foreach (JournalSlot slot in Player.Journal.Slots)
        {
            Document document = slot.Document;
            GameObject documentSlot = Instantiate(Resources.Load<GameObject>("Prefabs/Document"), _documentContainer, false);
            documentSlot.transform.Find("DocumentName").GetComponent<TextMeshProUGUI>().text = document.EntityScriptableObject.Name;
            documentSlot.GetComponent<Button>().onClick.AddListener(() => OptionsPanelController.Instance.OpenPanel(document));
        }
    }

    private void ClosePanel()
    {
        enabled = false;
    }
}
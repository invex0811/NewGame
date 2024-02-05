using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
using System;

public class InspectionController : MonoBehaviour, IDragHandler
{
    private GameObject _inspectablePrefab;
    private Entity _item;

    public static InspectionController Instance;

    public GameObject InspectionPanel;
    public Camera InspectionCamera;
    public Button CloseInspectionPanelButton;
    public TextMeshProUGUI ObjectName;
    public TextMeshProUGUI ObjectDescription;

    private void Awake()
    {
        Instance = this;
    }

    private void CloseInspectionPanel()
    {
        CloseInspectionPanelButton.onClick.RemoveAllListeners();

        InspectionPanel.SetActive(false);
        InspectionCamera.enabled = false;

        if(GameManager.TypeOfControl == TypesOfControl.InspectionControll)
        {
            GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
            PlayerController.Instance.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_inspectablePrefab != null)
            _inspectablePrefab.transform.eulerAngles += new Vector3(-eventData.delta.y / 3, -eventData.delta.x / 3);
    }
    public void Initialize(int objectID)
    {
        _item = EntitiesList.Entities[objectID];

        if (_inspectablePrefab != null)
            Destroy(_inspectablePrefab);

        _inspectablePrefab = Instantiate(_item.Prefab, new Vector3(1000, 1000, 1000), new Quaternion(0, 180, 0, 0));

        CloseInspectionPanelButton.onClick.AddListener(() => CloseInspectionPanel());

        InspectionCamera.enabled = true;
        InspectionPanel.SetActive(true);

        ObjectName.text = _item.DisplayName;
        ObjectDescription.text = _item.Description;
    }
}
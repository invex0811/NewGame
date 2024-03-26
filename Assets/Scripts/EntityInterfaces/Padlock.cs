using System.Collections;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private char[] _password;

    private PadlockRing[] _rings;
    private int _currentRingIndex;
    private char[] _currentCombination;

    private void OnEnable()
    {
        _rings = GetComponentsInChildren<PadlockRing>();
        _currentCombination = new char[_rings.Length];
        foreach (PadlockRing ring in _rings)
        {
            ring.OnValueChange += UpdateCombination;
            ring.enabled = true;
        }
    }
    private void OnDisable()
    {
        foreach (PadlockRing ring in _rings)
        {
            ring.OnValueChange -= UpdateCombination;
            ring.enabled = false;
        }

        _rings = null;
    }
    private void Update()
    {
        if(InteractionController.Instance.CurrentInteraction != InteractionType.SafePadlock)
            enabled = false;

        if (Input.GetKeyDown(KeyCode.A))
            ActivatePreviousRing();
        if (Input.GetKeyDown(KeyCode.D))
            ActivateNextRing();
    }

    private void ActivatePreviousRing()
    {
        _rings[_currentRingIndex].Deactivate();
        _rings[_currentRingIndex].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe2/TexturesMaterials/PadlockMat");

        if (_currentRingIndex == 0)
            _currentRingIndex = _rings.Length - 1;
        else
            _currentRingIndex--;

        _rings[_currentRingIndex].Activate();
        _rings[_currentRingIndex].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe2/TexturesMaterials/PadlockHighlightedMat");
    }
    private void ActivateNextRing()
    {
        _rings[_currentRingIndex].Deactivate();
        _rings[_currentRingIndex].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe2/TexturesMaterials/PadlockMat");

        if (_currentRingIndex == _rings.Length - 1)
            _currentRingIndex = 0;
        else
            _currentRingIndex++;

        _rings[_currentRingIndex].Activate();
        _rings[_currentRingIndex].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe2/TexturesMaterials/PadlockHighlightedMat");
    }
    private void UpdateCombination(int index, char value)
    {
        _currentCombination[index] = value;

        string currentCombination = "";
        string password = "";

        foreach (char item in _currentCombination)
            currentCombination += item;
        foreach (char item in _password)
            password += item;

        if (currentCombination == password)
        { 
            gameObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            gameObject.GetComponent<Animator>().Play("PadlockOpening");
        }
    }
    private void OpenDoor()
    {
        GetComponentInParent<Entity>().gameObject.layer = 0;

        _door.Open();

        enabled = false;
        Destroy(gameObject);
    }
}
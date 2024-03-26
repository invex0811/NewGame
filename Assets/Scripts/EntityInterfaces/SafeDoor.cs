using System.Collections;
using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private string _password;

    private string _enteredCombination;
    private SafeButtonController[] _buttons;

    public bool IsLocked
    {
        get => _isLocked;
    }

    private void OnEnable()
    {
        _buttons = GetComponentsInChildren<SafeButtonController>();
        foreach (SafeButtonController button in _buttons)
        {
            button.enabled = true;
            button.OnButtonPressed += ComparePassword;
        }
        GetComponentInChildren<SafeHandleController>().enabled = true;
        GetComponentInChildren<SafeHandleController>().OnHandleEndRotation += UpdateState;

        _enteredCombination = "";
    }
    private void OnDisable()
    {
        GetComponentInChildren<SafeHandleController>().OnHandleEndRotation -= UpdateState;
        GetComponentInChildren<SafeHandleController>().enabled = false;
        foreach (SafeButtonController button in _buttons)
        {
            button.OnButtonPressed -= ComparePassword;
            button.enabled = false;
        }
        _buttons = null;
    }

    private void ComparePassword(string character)
    {
        _enteredCombination += character;

        if (_enteredCombination.Length > _password.Length)
        {
            _enteredCombination = character;
            _isLocked = true;
        }

        if (_enteredCombination == _password)
        {
            _isLocked = false;
            return;
        }
    }
    private void UpdateState()
    {
        if (_isLocked)
        {
            _enteredCombination = "";
            return;
        }

        GetComponent<Door>().Open();

        enabled = false;
    }
}
using UnityEngine;

public class SafeDigital : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private string _password;
    [SerializeField] private Door _door;
    [SerializeField] private SafeHandle _handle;

    private string _enteredCombination;
    private SafeButton[] _buttons;

    public bool IsLocked
    {
        get => _isLocked;
    }

    private void OnEnable()
    {
        _buttons = GetComponentsInChildren<SafeButton>();
        foreach (SafeButton button in _buttons)
        {
            button.enabled = true;
            button.OnButtonPressed += ComparePassword;
        }

        _handle.enabled = true;
        _handle.OnHandleEndRotation += UpdateState;

        _enteredCombination = "";
    }
    private void OnDisable()
    {
        _handle.OnHandleEndRotation -= UpdateState;
        _handle.enabled = false;
        foreach (SafeButton button in _buttons)
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

        _door.Open();

        enabled = false;
    }
}
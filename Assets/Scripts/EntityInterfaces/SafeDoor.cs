using System.Collections;
using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private string _password;
    [SerializeField] private float _rotatingSpeed;

    private float _deltaTime;
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

        OpenDoor();
    }
    private void OpenDoor()
    {
        StartCoroutine(RotateDoor());
        enabled = false;
    }
    private IEnumerator RotateDoor()
    {
        while (_deltaTime < _rotatingSpeed)
        {
            gameObject.transform.Rotate(Vector3.back, 90 / _rotatingSpeed);
            _deltaTime++;
            yield return null;
        }
        _deltaTime = 0;

        yield return new WaitForSeconds(5f);

        yield break;
    }
}
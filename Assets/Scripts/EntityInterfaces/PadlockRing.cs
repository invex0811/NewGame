using System.Collections;
using UnityEngine;

public class PadlockRing : MonoBehaviour
{
    [SerializeField] private char[] _values;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _currentValue;
    [SerializeField] private int _ringIndex;
    [SerializeField] private bool _isActive;

    private float _deltaTime = 0;

    private void OnEnable()
    {
        OnValueChange?.Invoke(_ringIndex, _values[_currentValue]);

        if (_isActive)
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe2/TexturesMaterials/PadlockHighlightedMat");
    }
    private void Update()
    {
        if (!_isActive)
            return;

        if(Input.GetKeyDown(KeyCode.W))
            SetNextValue();
        if(Input.GetKeyDown(KeyCode.S))
            SetPreviousValue();
    }

    private void SetNextValue()
    {
        if (_deltaTime != 0)
            return;

        if (_currentValue == _values.Length - 1)
            _currentValue = 0;
        else
            _currentValue++;

        OnValueChange?.Invoke(_ringIndex, _values[_currentValue]);

        StartCoroutine(Rotate(Vector3.forward));
    }
    private void SetPreviousValue()
    {
        if (_deltaTime != 0)
            return;

        if (_currentValue == 0)
            _currentValue = _values.Length - 1;
        else
            _currentValue--;

        OnValueChange?.Invoke(_ringIndex, _values[_currentValue]);

        StartCoroutine(Rotate(Vector3.back));
    }
    private IEnumerator Rotate(Vector3 direction)
    {
        while(_deltaTime < _rotationDuration)
        {
            gameObject.transform.Rotate(direction, _rotationSpeed);
            _deltaTime++;
            yield return null;
        }

        _deltaTime = 0;

        yield break;
    }
    
    public void Activate()
    {
        _isActive = true;
    }
    public void Deactivate()
    {
        _isActive = false;
    }

    public delegate void ValueChangeHandler(int index, char value);
    public event ValueChangeHandler OnValueChange;
}
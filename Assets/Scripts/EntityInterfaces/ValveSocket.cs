using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ValveSocket : MonoBehaviour
{
    [SerializeField] private GameObject _valve;
    [SerializeField] private GameObject _smoke;
    [SerializeField, Tooltip("Degrees per frame.")] private float _rotationSpeed;
    [SerializeField] private Axis _rotationAxis;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private bool _isOpen;

    private float _deltaTime = 0;
    private float _rotationTime;

    public bool IsOpen
    {
        get => _isOpen;
    }

    private IEnumerator Rotate()
    {
        Vector3 axis = _rotationAxis switch
        {
            Axis.X => Vector3.right,
            Axis.Y => Vector3.up,
            Axis.Z => Vector3.forward,
            Axis.NegativeX => Vector3.left,
            Axis.NegativeY => Vector3.down,
            Axis.NegativeZ => Vector3.back,
            _ => Vector3.right,
        };

        _rotationSpeed = math.abs(_rotationSpeed);
        _rotationTime = _rotationAngle / _rotationSpeed;

        if (!_isOpen)
            _rotationSpeed = -_rotationSpeed;

        while (_deltaTime < _rotationTime)
        {
            _valve.transform.Rotate(axis, _rotationSpeed);
            _deltaTime++;

            yield return new WaitForFixedUpdate();
        }
        _deltaTime = 0;

        _isOpen = !_isOpen;

        _smoke.SetActive(_isOpen);

        enabled = false;

        yield break;
    }

    public void SetValve(GameObject prefab)
    {
        _valve = Instantiate(prefab, transform.parent.GetComponent<Transform>());
        gameObject.layer = 0;
    }
    public void Interact()
    {
        if (_deltaTime != 0)
            return;

        StartCoroutine(Rotate());
    }
}
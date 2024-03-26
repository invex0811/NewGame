using System.Collections;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField, Tooltip("Degrees per frame.")] private float _rotationSpeed;
    [SerializeField] private Axis _rotationAxis;
    [SerializeField] private float _rotationAngle;

    private float _deltaTime = 0;
    private float _rotationTime;
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

        _rotationTime = _rotationAngle / _rotationSpeed;

        while (_deltaTime < _rotationTime)
        {
            gameObject.transform.Rotate(axis, _rotationSpeed);
            _deltaTime++;

            yield return new WaitForFixedUpdate();
        }
        _deltaTime = 0;

        enabled = false;

        yield break;
    }
    public void Interact()
    {
        if (_deltaTime != 0)
            return;

        StartCoroutine(Rotate());
    }
}
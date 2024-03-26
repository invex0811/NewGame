using System.Collections;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationTime;
    [SerializeField] private bool _changeRotationDirection;

    private float _deltaTime = 0;
    private IEnumerator Rotate()
    {
        while (_deltaTime < _rotationTime)
        {
            float angle = _rotationAngle / _rotationTime;

            if (_changeRotationDirection)
                angle *= -1;

            gameObject.transform.Rotate(Vector3.forward, angle);
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
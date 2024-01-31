using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform _rotatingLeaf;
    [SerializeField] private AnimationCurve _openingCurve;
    [SerializeField] private AnimationCurve _closingCurve;
    [SerializeField] private float _duration = 2.0f;
    private Coroutine _rotateCoroutine;
    private bool _isDoorOpen = false;

    private void Start()
    {
        Close(); // Начальное состояние - дверь закрыта
    }

    private IEnumerator Rotate(float start, float end, AnimationCurve curve)
    {
        float timer = 0f;

        while (timer < _duration)
        {
            float progress = timer / _duration;
            _rotatingLeaf.transform.rotation = Quaternion.Lerp(
                Quaternion.Euler(0, start, 0),
                Quaternion.Euler(0, end, 0),
                curve.Evaluate(progress));

            timer += Time.deltaTime;
            yield return null;
        }

        _rotatingLeaf.transform.rotation = Quaternion.Euler(0, end, 0);
        _rotateCoroutine = null;

        if (end == 0)
        {
            _isDoorOpen = false;
        }
    }

    [SerializeField] private float _openAngle = 110.0f;
    [SerializeField] private float _openAngleMinus = -110.0f;

    private float GetCurrentAngle()
    {
        Vector3 currentDirection = _rotatingLeaf.transform.forward;
        float currentAngle = Vector3.SignedAngle(Vector3.forward, currentDirection, Vector3.up);
        return _openAngle > 0 ? currentAngle : -currentAngle;
    }

    private DoorState GetDoorState(float angle)
    {
        if (Mathf.Approximately(0, angle))
            return DoorState.Close;

        if (Mathf.Approximately(_openAngle, angle))
            return DoorState.Left;
        if (Mathf.Approximately(_openAngleMinus, angle))
            return DoorState.Right;

        return DoorState.Undefined;
    }

    private enum DoorState
    {
        Undefined,
        Left,
        Right,
        Close,
    }
    //213123
    public void Left()
    {
        var currentAngle = GetCurrentAngle();

        if (GetDoorState(currentAngle) == DoorState.Left || _isDoorOpen)
            return;

        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(Rotate(currentAngle, _openAngle, _openingCurve));
        _isDoorOpen = true;
    }

    public void Right()
    {
        var currentAngle = GetCurrentAngle();

        if (GetDoorState(currentAngle) == DoorState.Right || _isDoorOpen)
            return;

        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(Rotate(currentAngle, _openAngleMinus, _openingCurve));
        _isDoorOpen = true;
    }

    public void Close()
    {
        var currentAngle = GetCurrentAngle();

        if (GetDoorState(currentAngle) == DoorState.Close || !_isDoorOpen)
            return;

        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(Rotate(currentAngle, 0, _closingCurve));
        _isDoorOpen = false;
    }

    public void ToggleRight()
    {
        var currentAngle = GetCurrentAngle();

        if (GetDoorState(currentAngle) == DoorState.Close)
            Right();

        else if (GetDoorState(currentAngle) == DoorState.Right)
            Close();
    }

    public void ToggleLeft()
    {
        var currentAngle = GetCurrentAngle();

        if (GetDoorState(currentAngle) == DoorState.Close)
            Left();
        else if (GetDoorState(currentAngle) == DoorState.Left)
            Close();
    }
}

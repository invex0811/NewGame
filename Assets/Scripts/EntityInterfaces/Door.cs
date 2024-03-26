using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
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

    public void Open()
    {
        if (_deltaTime != 0)
            return;

        if (!_isOpen)
            StopAllCoroutines();

        StartCoroutine(Rotate());
    }
    public void Close()
    {
        if (_deltaTime != 0)
            return;

        StartCoroutine(Rotate());
    }
    private IEnumerator Rotate()
    {
        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.DoorOpening), gameObject.GetComponent<AudioSource>());

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

        if (_isOpen)
            axis *= -1;

        _rotationTime = _rotationAngle / _rotationSpeed;

        while (_deltaTime < _rotationTime)
        {
            gameObject.transform.Rotate(axis, _rotationSpeed);
            _deltaTime++;

            yield return new WaitForFixedUpdate();
        }
        _deltaTime = 0;
        _isOpen = !_isOpen;

        yield break;
    }
}

public enum Axis { X, Y, Z, NegativeX, NegativeY, NegativeZ }
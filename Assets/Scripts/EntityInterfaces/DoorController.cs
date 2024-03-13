using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool _isOpen;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _timeToCloseAutomaticly;

    private float _deltaTime = 0;

    public bool IsOpen
    {
        get => _isOpen;
    }

    public void OpenDoor()
    {
        if (_deltaTime != 0)
            return;

        if (!_isOpen)
            StopAllCoroutines();

        StartCoroutine(RotateDoor(90 / _rotatingSpeed));
    }
    public void CloseDoor()
    {
        if (_deltaTime != 0)
            return;

        StartCoroutine(RotateDoor(-90 / _rotatingSpeed));
    }
    private IEnumerator RotateDoor(float angle)
    {
        GlobalAudioService.PlayAudio(AudioLibrary.Sounds[Sound.DoorOpening], gameObject.GetComponent<AudioSource>());
        while (_deltaTime < _rotatingSpeed)
        {
            gameObject.transform.Rotate(Vector3.up, angle);
            _deltaTime++;
            yield return null;
        }
        _deltaTime = 0;
        _isOpen = !_isOpen;

        yield return new WaitForSeconds(5f);

        if (_isOpen)
        {
            CloseDoor();
        }

        yield break;
    }
}
using System.Collections;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _horizontalIncline;
    [SerializeField] private float _verticalIncline;
    [SerializeField] private float _delay;

    private void Update()
    {
        if (_delay == 0)
        {
            Rotate(_camera.rotation);
            return;
        }

        StartCoroutine(DelayRotation(_camera.rotation));
    }

    private IEnumerator DelayRotation(Quaternion rotation)
    {
        yield return new WaitForSecondsRealtime(_delay);

        Rotate(rotation);

        yield break;
    }
    private void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(-transform.eulerAngles.x - 90 + _verticalIncline, transform.eulerAngles.y + _horizontalIncline - 180, transform.eulerAngles.z);
    }
}
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
        StartCoroutine(Rotate(_camera.rotation));
    }

    private IEnumerator Rotate(Quaternion rotation)
    {
        yield return new WaitForSecondsRealtime(_delay);

        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(-transform.eulerAngles.x - _horizontalIncline - 90, transform.eulerAngles.y + _verticalIncline - 180, transform.eulerAngles.z);

        yield break;
    }
}
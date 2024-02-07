using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 1.0f;
    [SerializeField] private float _maxYAngle = 80.0f;

    private float _rotationVertical = 0.0f;
    private bool _rotationEnabled = true;

    public static CameraController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (GameManager.TypeOfControl != TypesOfControl.PlayerControl)
            return;
        if (!_rotationEnabled)
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.parent.Rotate(Vector3.up * mouseX * _sensitivity);

        _rotationVertical -= mouseY * _sensitivity;
        _rotationVertical = Mathf.Clamp(_rotationVertical, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(_rotationVertical, 0.0f, 0.0f);
    }
}
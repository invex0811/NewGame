using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _rotationVertical = 0.0f;
    private bool _rotationEnabled = true;

    public float sensitivity = 1.0f;
    public float maxYAngle = 80.0f;
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

        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        _rotationVertical -= mouseY * sensitivity;
        _rotationVertical = Mathf.Clamp(_rotationVertical, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(_rotationVertical, 0.0f, 0.0f);
    }

    public void EnableRotation()
    {
        _rotationEnabled = true;
    }
    public void DisableRotation()
    {
        _rotationEnabled = false;
    }
}
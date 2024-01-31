using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float maxYAngle = 80.0f;
    public static CameraController instance;

    private float rotationX = 0.0f;
    private bool rotationEnabled = true;

    private void Update()
    {
        if (!rotationEnabled)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }

    public void EnableRotation()
    {
        rotationEnabled = true;
    }

    public void DisableRotation()
    {
        rotationEnabled = false;
    }

    private void Awake()
    {
        instance = this;
    }
}

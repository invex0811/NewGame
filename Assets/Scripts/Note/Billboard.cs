using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Получаем текущий поворот объекта
            Quaternion currentRotation = transform.rotation;

            // Вращаем объект так, чтобы он смотрел на камеру
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);

            // Поддерживаем исходный поворот по оси Y
            transform.rotation = Quaternion.Euler(new Vector3(currentRotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotation.eulerAngles.z));
        }
    }
}

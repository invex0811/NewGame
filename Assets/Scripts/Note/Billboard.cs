using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera MainCamera;

    void LateUpdate()
    {
        if (MainCamera != null)
        {
            Vector3 direction = MainCamera.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.down);
        }
    }
}
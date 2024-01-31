using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;

    private void OnTriggerEnter(Collider other)
    {
        UpdateDoorState(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        _doorController.Close();
    }

    private void UpdateDoorState(Vector3 playerPosition)
    {
        Vector3 directionToPlayer = playerPosition - transform.position;
        Vector3 forwardDirection = transform.forward;

        float angle = Vector3.SignedAngle(directionToPlayer, forwardDirection, Vector3.up);

        if (angle > 0)
        {
            _doorController.Right(); // Поменял местами вызов методов
        }
        else
        {
            _doorController.Left(); // Поменял местами вызов методов
        }
    }
}

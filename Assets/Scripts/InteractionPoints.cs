using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    [SerializeField] private GameObject _initialCameraPosition;
    [SerializeField] private GameObject _tv;

    public static InteractionPoints Instance;

    public GameObject InitialCameraPosition
    {
        get => _initialCameraPosition;
    }
    public GameObject TV
    {
        get => _tv;
    }

    private void Awake()
    {
        Instance = this;
    }
}
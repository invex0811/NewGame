using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    [SerializeField] private GameObject _initialCameraPosition;
    [SerializeField] private GameObject _tv;
    [SerializeField] private GameObject _safeNumeric;

    public static InteractionPoints Instance;

    public GameObject InitialCameraPosition
    {
        get => _initialCameraPosition;
    }
    public GameObject TV
    {
        get => _tv;
    }
    public GameObject SafeNumeric
    {
        get => _safeNumeric;
    }

    private void Awake()
    {
        Instance = this;
    }
}
using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    [SerializeField] private GameObject _tv;
    [SerializeField] private GameObject _safeDigital;
    [SerializeField] private GameObject _safePadlock;

    public static InteractionPoints Instance;

    public GameObject TV
    {
        get => _tv;
    }
    public GameObject SafeDigital
    {
        get => _safeDigital;
    }
    public GameObject SafePadlock
    {
        get => _safePadlock;
    }

    private void Awake()
    {
        Instance = this;
    }
}
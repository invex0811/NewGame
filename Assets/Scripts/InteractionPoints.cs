using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    public static InteractionPoints Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject TV;
}
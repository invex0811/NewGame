using UnityEngine;

public class InteractionPoints : MonoBehaviour
{
    public static InteractionPoints Instance;

    public GameObject TV;

    private void Awake()
    {
        Instance = this;
    }
}
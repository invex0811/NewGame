using UnityEngine;
using UnityEngine.EventSystems;

public class SafeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 _targerPosition;
    [SerializeField] private string _value;
    private Vector3 _initialPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        _initialPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = _targerPosition;
        OnButtonPressed?.Invoke(_value);

        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), gameObject.GetComponent<AudioSource>());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.transform.localPosition = _initialPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe/Textures/SafeMatHighlighted");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Objects/Safe/Textures/SafeMat");
    }

    public delegate void ButtonPressedEventHandler(string value);
    public event ButtonPressedEventHandler OnButtonPressed;
}
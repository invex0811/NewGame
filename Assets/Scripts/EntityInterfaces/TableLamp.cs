using UnityEngine;

public class TableLamp : MonoBehaviour
{
    [SerializeField] private bool _isOn;
    [SerializeField] private Color _litColor;
    [SerializeField] private Color _unlitColor;
    [SerializeField] private GameObject _lightSource;

    private void Start()
    {
        _lightSource.GetComponent<Light>().enabled = _isOn;
        SetEmission();
    }

    private void SetEmission()
    {

        if (_isOn)
            GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissiveColor", _litColor * 24);
        else
            GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissiveColor", _unlitColor * 24);
    }

    public void Toogle()
    {
        GlobalAudioService.PlayAudio(AudioProvider.GetSound(Sound.ButtonClick), gameObject.GetComponent<AudioSource>());

        _isOn = !_isOn;
        _lightSource.GetComponent<Light>().enabled = _isOn;

        SetEmission();
    }
}
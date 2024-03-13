using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Document")]
public class DocumentScriptableObject : EntityScriptableObject
{
    [SerializeField] private string _text;

    public string Text => _text;
}
using System.Collections;
using UnityEngine;

class Door : Entity
{
    private readonly int _id;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;
    private float _deltaTime;

    public override int ID => _id;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;

    public Door(int id, string displayName, string description, string raycastFeedbackText, GameObject prefab)
    {
        _id = id;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override void Interact(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.Play("DoorOpen");
            _deltaTime = Time.time;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).length > Time.time - _deltaTime)
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
        {
            animator.Play("DoorOpen");
            _deltaTime = Time.time;
            return;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            animator.Play("DoorClose");
            _deltaTime = Time.time;
            return;
        }
    }
}
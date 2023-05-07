using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTargetMediator : MonoBehaviour
{
    [SerializeField] private GameObject _interaction;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private TargetSelector _targetSelector;

    private Interactor[] _interactors;
    private RingBuffer<Transform> _targetBuffer;

    private Transform _currentTarget;

    void Awake()
    {
        _interactors = _interaction.GetComponents<Interactor>();
        _targetBuffer = new RingBuffer<Transform>(_targets.ToArray());
        _currentTarget = _targets[0];
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            _targetBuffer.IterateDown();
            SetInteractionTarget(_targetBuffer.Get());
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            _targetBuffer.IterateUp();
            SetInteractionTarget(_targetBuffer.Get());
        }
    }

    public List<Transform> GetTargets()
    {
        return _targets;
    }

    public Transform GetCurrentTarget()
    {
        return _currentTarget;
    }

    public void AddTarget(Transform target)
    {
        _targets.Add(target);
        ClearTargetBuffer();
    }

    public void RemoveTarget(Transform target)
    {
        _targets.Remove(target);
        ClearTargetBuffer();

        if (_currentTarget == target)
        {
            SetInteractionTarget(_targetBuffer.Get());
        }
    }

    public void SetInteractionTarget(Transform target)
    {
        foreach (var interactor in _interactors)
        {
            interactor.SetTarget(target);
        }

        _currentTarget = target;
    }
    private void ClearTargetBuffer()
    {
        int at = _targetBuffer.GetAt();
        _targetBuffer = new RingBuffer<Transform>(_targets.ToArray());
        _targetBuffer.SetAt(at);
        _targetSelector.ClearInteractionTargets();
    }
}

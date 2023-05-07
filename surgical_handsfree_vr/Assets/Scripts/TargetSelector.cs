using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : Interactor
{
    [SerializeField] private InteractionTargetMediator _interactionTargetMediator;
    [SerializeField] private float _threshold;
    [SerializeField] private Transform _currentTarget;

    private Camera _camera;
    private List<Transform> _targets;

    void Awake()
    {
        _camera = Camera.main;
    }

    void OnEnable()
    {
        _targets = _interactionTargetMediator.GetTargets();
    }

    void OnDisable()
    {
        if (_currentTarget)
        {
            _currentTarget.GetComponent<SelectorMaterialChanger>()?.Unselect();
        }
    }

    void Update()
    {
        Transform interactionTarget = CheckInteractionTarget();
        if (interactionTarget != null && interactionTarget != _currentTarget)
        {
            ChangeInteractionTarget(interactionTarget);
        }
    }

    public void ClearInteractionTargets()
    {
        _targets = _interactionTargetMediator.GetTargets();
    }

    private void ChangeInteractionTarget(Transform interactionTarget)
    {
        _interactionTargetMediator.SetInteractionTarget(interactionTarget);
        _currentTarget.GetComponent<SelectorMaterialChanger>()?.Unselect();
        interactionTarget.GetComponent<SelectorMaterialChanger>()?.Select();
        _currentTarget = interactionTarget;
    }

    private Transform CheckInteractionTarget()
    {
        float biggestDotProduct = 0f;
        Transform interactionTarget = null;

        foreach (var target in _targets)
        {
            Vector3 camForward = _camera.transform.forward.normalized;
            Vector3 camToObject = target.position - _camera.transform.position;

            float dotProduct = Vector3.Dot(camForward.normalized, camToObject.normalized);
            if (dotProduct >= _threshold && dotProduct > biggestDotProduct)
            {
                biggestDotProduct = dotProduct;
                interactionTarget = target;
            }
        }

        return interactionTarget;
    }
}

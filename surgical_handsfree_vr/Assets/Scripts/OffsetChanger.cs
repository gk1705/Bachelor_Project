using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetChanger : Interactor
{
    [SerializeField] private Snapper _snapper;
    [SerializeField] private float _dotThreshold;
    [SerializeField] private float _speed;

    private Transform _cameraTransform;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForwardNormalized = _cameraTransform.forward.normalized;
        float dotProduct = Vector3.Dot(cameraForwardNormalized, Vector3.up);

        if (Mathf.Abs(dotProduct) > _dotThreshold)
        {
            float distanceAdjustment = 0;
            if (_dotThreshold < 0)
            {
                distanceAdjustment = dotProduct - _dotThreshold;
            }
            else
            {
                distanceAdjustment = dotProduct + _dotThreshold;
            }

            _snapper.AdjustDistance(distanceAdjustment * _speed);
        }

        _snapper.SnapToView();
    }
}

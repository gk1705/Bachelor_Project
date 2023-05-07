using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : Interactor
{
    private Camera _camera = null;
    [SerializeField] private float _distance;
    [SerializeField] private float _dampening;
    [SerializeField] private float _minimumDistance;
    [SerializeField] private float _maximumDistance;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        SnapToView();
    }

    public void SnapPositionToView()
    {
        _target.position = Vector3.Lerp(_target.position, _camera.transform.position + (_camera.transform.forward.normalized * _distance), _dampening);
    }

    public void SnapRotationToView()
    {
        var targetToCamera = _camera.transform.position - _target.transform.position;
        _target.rotation = Quaternion.Lerp(_target.rotation, Quaternion.LookRotation(targetToCamera, _camera.transform.up), _dampening);
    }

    public void SnapToView()
    {
        SnapPositionToView();
        SnapRotationToView();
    }

    public void AdjustDistance(float adjustment)
    {
        float newDistance = _distance + adjustment;
        if (newDistance >= _minimumDistance && newDistance <= _maximumDistance)
        {
            _distance = newDistance;
        }
    }
}

//#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotator : Interactor
{
    [SerializeField] private float _angle;
    [SerializeField] private float _rotationDistanceThreshold;
    [SerializeField] private float _resetDistanceThreshold;
    [SerializeField] private float _resetRotationSpeed;
    [SerializeField] private float _rotationSpeed;

    private Plane _intersectionPlane;
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        _intersectionPlane = new Plane(-(_camera.transform.forward), _target.transform.position);

        if (_intersectionPlane.Raycast(ray, out var distance))
        {
            var intersectionPoint = ray.GetPoint(distance);

            var targetRotation = CalculateTargetRotation(intersectionPoint);
            var pointDistance = DistanceBetweenIntersectionPoints(intersectionPoint);

            RotateTarget(pointDistance, targetRotation);
        }
    }

    private Quaternion CalculateTargetRotation(Vector3 intersectionPoint)
    {
        var camToIntersectionPoint = intersectionPoint - _camera.transform.position;
        var camToTarget = _target.transform.position - _camera.transform.position;

        float distance = Vector3.Distance(camToIntersectionPoint, camToTarget);

#if DEBUG
        DrawDebugLines(camToIntersectionPoint, camToTarget);
#endif

        return Quaternion.FromToRotation(camToIntersectionPoint, camToTarget);
    }

    private void DrawDebugLines(Vector3 camToIntersectionPoint, Vector3 camToTarget)
    {
        Debug.DrawLine(_camera.transform.position, camToIntersectionPoint);
        Debug.DrawLine(_camera.transform.position, camToTarget);
    }

    private float DistanceBetweenIntersectionPoints(Vector3 intersectionPoint)
    {
        var camToIntersectionPoint = intersectionPoint - _camera.transform.position;
        var camToTarget = _target.transform.position - _camera.transform.position;

        return Vector3.Distance(camToIntersectionPoint, camToTarget);
    }

    private void RotateTarget(float distance, Quaternion targetRotation)
    {
        if (distance >= _rotationDistanceThreshold)
        {
            RotateToTargetRotation(targetRotation);
        }
        else if (distance <= _resetDistanceThreshold)
        {
            //ResetTargetRotation();
        }
    }

    private void RotateToTargetRotation(Quaternion targetRotation)
    {
        _target.transform.rotation = Quaternion.Slerp(_target.transform.rotation, targetRotation * _target.transform.rotation, _rotationSpeed * Time.deltaTime);
    }

    private void ResetTargetRotation()
    {
        _target.transform.rotation = Quaternion.Slerp(_target.transform.rotation, Quaternion.identity, _resetRotationSpeed * Time.deltaTime);
    }
}

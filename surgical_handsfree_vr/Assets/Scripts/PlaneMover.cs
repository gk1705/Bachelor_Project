using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMover : Interactor
{
    [SerializeField] private Snapper _snapper;
    [SerializeField] private float _distanceThreshold;
    //[SerializeField] private float _targetMovementSpeed;
    [SerializeField] private float _dampening;
    [SerializeField] private GameObject _illustrationPlane;

    private BoxCollider _planeCollider;// _targetCollider;
    //private Plane _intersectionPlane;

    private Camera _camera;
    private Vector3 _movement;

    void Awake()
    {
        _camera = Camera.main;
        _planeCollider = _illustrationPlane.GetComponent<BoxCollider>();
        //_targetCollider = _target.GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ActivatePlane();
    }

    private void CenterPlane()
    {
        //_intersectionPlane = new Plane(-(_camera.transform.forward), _target.transform.position);
        _illustrationPlane.transform.position = _target.transform.position;
        _illustrationPlane.transform.rotation = Quaternion.LookRotation(-(_camera.transform.forward), _camera.transform.up) * Quaternion.AngleAxis(90f, Vector3.right);
    }

    private void ActivatePlane()
    {
        CenterPlane();
        _illustrationPlane.SetActive(true);
    }

    private void DeactivatePlane()
    {
        _illustrationPlane.SetActive(false);
    }

    void OnEnable()
    {
        ActivatePlane();
    }

    void OnDisable()
    {
        DeactivatePlane();
    }

    // Update is called once per frame
    void Update()
    {
        /*Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (_intersectionPlane.Raycast(ray, out var distance))
        {
            var intersectionPoint = ray.GetPoint(distance);
            _movement = CalculateTargetMovement(intersectionPoint);
            _target.transform.position += _movement * _targetMovementSpeed * Time.deltaTime;
        }*/


        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (_planeCollider.Raycast(ray, out var outHit, 100))
        {
            _target.position = Vector3.Lerp(_target.position, outHit.point, _dampening);
            _snapper.SnapRotationToView();
        }
    }

    /*private Vector3 CalculateTargetMovement(Vector3 intersectionPoint)
    {
        var camToIntersectionPoint = intersectionPoint - _camera.transform.position;
        var camToTarget = _target.transform.position - _camera.transform.position;

        float distance = Vector3.Distance(camToIntersectionPoint, camToTarget);

        if (distance > _distanceThreshold)
            return intersectionPoint - _target.transform.position;

        return Vector3.zero;
    }*/
}

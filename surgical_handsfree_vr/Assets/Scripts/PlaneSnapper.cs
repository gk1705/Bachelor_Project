using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSnapper : Interactor
{
    private Camera _camera = null;

    [SerializeField] private Snapper _snapper;
    [SerializeField] private float _distance;
    [SerializeField] private float _dampening;
    [SerializeField] private List<GameObject> _planes;
    private List<BoxCollider> _planeColliders;

    //private Plane _intersectionPlane;

    void Awake()
    {
        _camera = Camera.main;
        _planeColliders = new List<BoxCollider>();

        foreach (var plane in _planes)
        {
            var planeCollider = plane.GetComponent<BoxCollider>();
            _planeColliders.Add(planeCollider);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ActivatePlanes();
    }

    void OnEnable()
    {
       //ActivatePlanes();
    }

    void OnDisable()
    {
        //DeactivatePlanes();
    }

    private void ActivatePlanes()
    {
        foreach (var plane in _planes)
        {
            plane.SetActive(true);
        }
    }

    private void DeactivatePlanes()
    {
        foreach (var plane in _planes)
        {
            plane.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var colliderHits = GetColliderHits();
        if (colliderHits.Count != 0)
        {
            var closestColliderHit = GetClosestColliderHit(colliderHits);
            Debug.Assert(closestColliderHit != null);

            _target.position = Vector3.Lerp(_target.position, closestColliderHit.CollisionPoint, _dampening);
            _snapper.SnapRotationToView();
        }
    }

    public void AddPlane(GameObject plane)
    {
        _planes.Add(plane);
        var planeCollider = plane.GetComponent<BoxCollider>();
        _planeColliders.Add(planeCollider);
    }

    class ColliderHit
    {
        public Vector3 CollisionPoint;
        public BoxCollider Collider;
    }

    private List<ColliderHit> GetColliderHits()
    {
        List<ColliderHit> colliderHits = new List<ColliderHit>();

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        foreach (var planeCollider in _planeColliders)
        {
            if (planeCollider.Raycast(ray, out var outHit, _distance))
            {
                colliderHits.Add(new ColliderHit() { CollisionPoint = outHit.point, Collider = planeCollider });
            }
        }

        return colliderHits;
    }

    private ColliderHit GetClosestColliderHit(List<ColliderHit> colliderHits)
    {
        ColliderHit closestColliderHit = null;
        float closestDistance = _distance;

        foreach (var colliderHit in colliderHits)
        {
            float distance = Vector3.Distance(colliderHit.CollisionPoint, _camera.transform.position);
            if (distance < closestDistance)
            {
                closestColliderHit = colliderHit;
                closestDistance = distance;
            }
        }

        return closestColliderHit;
    }
}

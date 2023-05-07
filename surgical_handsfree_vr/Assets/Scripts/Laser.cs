using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform _focusPointTransform;

    private Transform _cameraTransform;
    private LineRenderer _lineRenderer;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, _cameraTransform.position);
        _lineRenderer.SetPosition(1, _focusPointTransform.position);
    }
}

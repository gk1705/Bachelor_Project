using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    private Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        _transform.position = _position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var toCamera = _camera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-toCamera, Vector3.up);
    }
}

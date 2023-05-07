using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoverEvent))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _angle;

    private Transform _target;

    public void Awake()
    {
        var gObject = GameObject.FindGameObjectWithTag("Heart");
        _target = gObject.transform;
    }

    public void RotateTarget()
    {
        _target.transform.Rotate(_axis, _angle * Time.deltaTime, Space.World);
    }

    public void ResetRotation()
    {
        _target.transform.rotation = Quaternion.identity;
    }
}

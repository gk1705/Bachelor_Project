using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationResetter : Interactor
{
    [SerializeField] private float _resetRotationSpeed;

    void Update()
    {
        ResetTargetRotation();
    }

    private void ResetTargetRotation()
    {
        _target.transform.rotation = Quaternion.Slerp(_target.transform.rotation, Quaternion.identity, _resetRotationSpeed * Time.deltaTime);
    }
}
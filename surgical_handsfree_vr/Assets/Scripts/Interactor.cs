using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    [SerializeField] protected Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}

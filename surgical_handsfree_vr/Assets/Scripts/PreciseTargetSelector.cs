using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseTargetSelector : MonoBehaviour
{
    [SerializeField] private InteractionTargetMediator _interactionTargetMediator;

    private Camera _camera;
    private List<Transform> _targets;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

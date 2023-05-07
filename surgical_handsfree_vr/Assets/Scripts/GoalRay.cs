using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRay : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _focusTimeThreshold;
    [SerializeField] private GoalManager _goalManager;

    private SelectorMaterialChanger _materialChanger;

    private Transform _cameraTransform;
    private float _focusTime;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out var hit, _maxDistance))
        {
            if (hit.collider.transform.gameObject.name == "FocusPoint")
            {
                _materialChanger = hit.collider.transform.gameObject.GetComponent<SelectorMaterialChanger>();
                _materialChanger.Select();

                _focusTime += Time.deltaTime;
                if (_focusTime >= _focusTimeThreshold)
                {
                    _goalManager.SetGoal(2, true);
                }
            }
            else
            {
                if (_materialChanger != null)
                {
                    _materialChanger.Unselect();
                    _materialChanger = null;
                }

                _focusTime = 0;
                _goalManager.SetGoal(2, false);
            }
        }
        else
        {
            if (_materialChanger != null)
            {
                _materialChanger.Unselect();
                _materialChanger = null;
            }

            _focusTime = 0;
            _goalManager.SetGoal(2, false);
        }
    }
}

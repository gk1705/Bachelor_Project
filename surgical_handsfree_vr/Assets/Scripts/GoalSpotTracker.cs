using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpotTracker : MonoBehaviour
{
    [SerializeField] private GoalManager _goalManager;
    [SerializeField] private SelectorMaterialChanger _materialChanger;
    
    //[SerializeField] private float _distanceThreshold;
    //private bool _goalSet;

    /*void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag != "Heart") return;

        float distance = Vector3.Distance(collider.transform.position, transform.position);
        if (distance < _distanceThreshold && !_goalSet)
        {
          _goalSet = true;
          _goalManager.SetGoal(1, _goalSet);
        }
        else if (_goalSet)
        {
            _goalSet = false;
            _goalManager.SetGoal(1, _goalSet);
        }
    }*/

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Heart") return;
        _goalManager.SetGoal(1, true);
        _materialChanger.Select();
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag != "Heart") return;
        _goalManager.SetGoal(1, false);
        _materialChanger.Unselect();
    }
}

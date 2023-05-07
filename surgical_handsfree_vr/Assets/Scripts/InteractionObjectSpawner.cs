using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectPrefabs;
    [SerializeField] private InteractionTargetMediator _interactionTargetMediator;
    [SerializeField] private InteractionMediator _interactionMediator;
    [SerializeField] private PlaneSnapper _planeSnapper;

    private Dictionary<string, GameObject> _objectPrefabMappings;

    void Awake()
    {
        _objectPrefabMappings = new Dictionary<string, GameObject>();
        foreach (var objectPrefab in _objectPrefabs)
        {
            _objectPrefabMappings.Add(objectPrefab.name.ToLower(), objectPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            Spawn("plane");
        }
    }

    public void Spawn(string objectName)
    {
        Transform targetTransform = _interactionTargetMediator.GetCurrentTarget();
        _interactionMediator.EnableInteraction(typeof(Snapper).ToString());
        GameObject objectPrefab = _objectPrefabMappings[objectName.ToLower()];
        GameObject newObject = GameObject.Instantiate(objectPrefab, targetTransform.position, objectPrefab.transform.localRotation);
        Transform newObjectTransform = newObject.transform;
        _interactionTargetMediator.AddTarget(newObjectTransform);
        _interactionTargetMediator.SetInteractionTarget(newObjectTransform);

        if (objectName.ToLower() == "plane")
        {
            _planeSnapper.AddPlane(newObject);
        }
    }
}

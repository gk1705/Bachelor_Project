using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

public class InteractionMediator : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private List<InteractionResponseTextMapping> _interactionResponseTextMappings;
    [SerializeField] private List<KeyCodeInteractionMapping> _keyCodeInteractionMappings;
    [SerializeField] private TextMediator _helpMediator;

    [SerializeField] private ChangeResponder _interactionChangeResponder;

    [System.Serializable]
    struct InteractionResponseTextMapping
    {
        public string Interaction;
        public string ResponseText;
    }
    private Dictionary<string, string> _responseTextMappings;

    [System.Serializable]
    struct KeyCodeInteractionMapping
    {
        public KeyCode KeyCode;
        public string InteractionKey;
    }
    private Dictionary<string, MonoBehaviour> _interactionBehaviourMappings;

    [SerializeField] private SteamVR_Action_Boolean _iterateInteractions;
    private RingBuffer<string> _interactionKeys;

    void Awake()
    {
        _responseTextMappings = new Dictionary<string, string>();
        foreach (var interactionResponseTextMapping in _interactionResponseTextMappings)
        {
            _responseTextMappings.Add(interactionResponseTextMapping.Interaction,
                interactionResponseTextMapping.ResponseText);
        }

        _interactionBehaviourMappings = new Dictionary<string, MonoBehaviour>();

        var snapper = _target.GetComponent<Snapper>();
        _interactionBehaviourMappings.Add(typeof(Snapper).ToString(), snapper);

        var planeSnapper = _target.GetComponent<PlaneSnapper>();
        _interactionBehaviourMappings.Add(typeof(PlaneSnapper).ToString(), planeSnapper);

        var planeRotator = _target.GetComponent<PlaneRotator>();
        _interactionBehaviourMappings.Add(typeof(PlaneRotator).ToString(), planeRotator);

        var planeMover = _target.GetComponent<PlaneMover>();
        _interactionBehaviourMappings.Add(typeof(PlaneMover).ToString(), planeMover);

        var targetSelector = _target.GetComponent<TargetSelector>();
        _interactionBehaviourMappings.Add(typeof(TargetSelector).ToString(), targetSelector);

        var offsetChanger = _target.GetComponent<OffsetChanger>();
        _interactionBehaviourMappings.Add(typeof(OffsetChanger).ToString(), offsetChanger);

        var idler = _target.GetComponent<Idler>();
        _interactionBehaviourMappings.Add(typeof(Idler).ToString(), idler);

        _interactionKeys = new RingBuffer<string>(_interactionBehaviourMappings.Keys.ToArray());

        var rotationResetter = _target.GetComponent<RotationResetter>();
        _interactionBehaviourMappings.Add(typeof(RotationResetter).ToString(), rotationResetter);
    }

    void Start()
    {
        EnableInteraction(typeof(Snapper).ToString());

        _iterateInteractions.AddOnStateDownListener(IterateInteractionDown, SteamVR_Input_Sources.LeftHand);
        _iterateInteractions.AddOnStateDownListener(IterateInteractionUp, SteamVR_Input_Sources.RightHand);
    }

    private void IterateInteractionDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _interactionKeys.IterateDown();
        EnableInteraction(_interactionKeys.Get());
    }

    private void IterateInteractionUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _interactionKeys.IterateUp();
        EnableInteraction(_interactionKeys.Get());
    }

    public void EnableInteraction(string interactionKey)
    {
        Debug.Assert(_interactionBehaviourMappings.ContainsKey(interactionKey));
        var interactionBehaviour = _interactionBehaviourMappings[interactionKey];
        interactionBehaviour.enabled = true;

        foreach (var mapping in _interactionBehaviourMappings)
        {
            if (mapping.Value == interactionBehaviour)
            {
                continue;
            }

            mapping.Value.enabled = false;
        }

        _helpMediator.SetText(interactionKey);
        TriggerInteractionResponse(interactionKey);
    }

    private void TriggerInteractionResponse(string interactionKey)
    {
        _interactionChangeResponder.TriggerResponse(_responseTextMappings[interactionKey]);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var mapping in _keyCodeInteractionMappings)
        {
            if (Input.GetKeyDown(mapping.KeyCode))
            {
                EnableInteraction(mapping.InteractionKey);
                break;
            }
        }
    }
}

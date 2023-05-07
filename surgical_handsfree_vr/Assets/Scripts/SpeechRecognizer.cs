using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognizer : MonoBehaviour
{
    [SerializeField] private InteractionMediator _interactionMediator;
    [SerializeField] private InteractionObjectSpawner _interactionObjectSpawner;
    [SerializeField] private TextMediator _helpMediator;

    [SerializeField] private ChangeResponder _interactionChangeResponder, _spawnResponder;

    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, Action> _actionDictionary;

    void Awake()
    {
        _actionDictionary = new Dictionary<string, Action>();
        AddActions();
        _keywordRecognizer = new KeywordRecognizer(_actionDictionary.Keys.ToArray(), ConfidenceLevel.Low);
        _keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        _keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs args)
    {
        _actionDictionary[args.text].Invoke();
    }

    // actions
    private void AddActions()
    {
        // snap front
        _actionDictionary.Add(
            "center", 
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(Snapper).ToString());
                //_interactionChangeResponder.TriggerResponse("Center");
            });

        // snap plane
        _actionDictionary.Add(
            "snap",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(PlaneSnapper).ToString());
                //_interactionChangeResponder.TriggerResponse("Snap");
            });

        // rotate
        _actionDictionary.Add(
            "rotate",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(PlaneRotator).ToString());
                //_interactionChangeResponder.TriggerResponse("Rotate");
            });

        // move
        _actionDictionary.Add(
            "move",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(PlaneMover).ToString());
                //_interactionChangeResponder.TriggerResponse("Move");
            });

        // reset
        _actionDictionary.Add(
            "reset",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(RotationResetter).ToString());
                //_interactionChangeResponder.TriggerResponse("Reset");
            });

        // select
        _actionDictionary.Add(
            "select",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(TargetSelector).ToString());
                //_interactionChangeResponder.TriggerResponse("Select");
            });

        // offset
        _actionDictionary.Add(
            "offset",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(OffsetChanger).ToString());
                //_interactionChangeResponder.TriggerResponse("Offset");
            });

        // idle
        _actionDictionary.Add(
            "stay",
            () =>
            {
                _interactionMediator.EnableInteraction(typeof(Idler).ToString());
                //_interactionChangeResponder.TriggerResponse("Idle");
            });

        //spawner
        _actionDictionary.Add(
            "heart",
            () =>
            {
                _interactionObjectSpawner.Spawn("heart");
                _spawnResponder.TriggerResponse("Heart");
            });
        _actionDictionary.Add(
            "plane",
            () =>
            {
                _interactionObjectSpawner.Spawn("plane");
                _spawnResponder.TriggerResponse("Plane");
            });
        _actionDictionary.Add(
            "billboard",
            () =>
            {
                _interactionObjectSpawner.Spawn("billboard");
                _spawnResponder.TriggerResponse("Billboard");
            });

        _actionDictionary.Add(
            "help",
            () => _helpMediator.ToggleBoard()
        );
    }
}

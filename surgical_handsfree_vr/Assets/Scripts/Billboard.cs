using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Billboard : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _holdBufferTimeThreshold;

    [SerializeField] private GoalManager _goalManager;
    [SerializeField] private int _goalImageIndex;

    [SerializeField] public SteamVR_Action_Boolean _iterateBillboardImages;

    private RingBuffer<Sprite> _spriteRingBuffer;

    private SteamVR_Input_Sources _currentlyPressed, _lastPressed;
    private float _holdBufferTime;

    void Awake()
    {
        _spriteRingBuffer = new RingBuffer<Sprite>(_sprites.ToArray());
        _slider.minValue = 0;
        _slider.maxValue = _sprites.Count - 1;

        _currentlyPressed = SteamVR_Input_Sources.Any;
        _lastPressed = SteamVR_Input_Sources.Any;

        _goalManager = FindObjectOfType<GoalManager>();
    }

    void Start()
    {
        _iterateBillboardImages.AddOnStateDownListener(IterateSpriteDown, SteamVR_Input_Sources.LeftHand);
        _iterateBillboardImages.AddOnStateDownListener(IterateSpriteUp, SteamVR_Input_Sources.RightHand);
        _iterateBillboardImages.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.LeftHand);
        _iterateBillboardImages.AddOnStateUpListener(GripUp, SteamVR_Input_Sources.RightHand);
    }

    private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == SteamVR_Input_Sources.LeftHand)
            _leftHeld = false;
        if (fromSource == SteamVR_Input_Sources.RightHand)
            _rightHeld = false;

        _holdBufferTime = 0;
    }

    private bool _leftHeld = false, _rightHeld = false;

    void Update()
    {
        CheckTriggerHold();
    }

    private void CheckTriggerHold()
    {
        if ((_leftHeld && _rightHeld) || (!_leftHeld && !_rightHeld))
        {
            _holdBufferTime = 0;
        }
        else
        {
            _holdBufferTime += Time.deltaTime;
        }

        if (_holdBufferTime >= _holdBufferTimeThreshold)
        {
            if (_leftHeld)
            {
                _spriteRingBuffer.IterateDown();
                UpdateBillboard();
            }
            else if (_rightHeld)
            {
                _spriteRingBuffer.IterateUp();
                UpdateBillboard();
            }
        }
    }

    private void IterateSpriteDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _spriteRingBuffer.IterateDown();
        UpdateBillboard();
        _leftHeld = true;
    }

    private void IterateSpriteUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _spriteRingBuffer.IterateUp();
        UpdateBillboard();
        _rightHeld = true;
    }

    private void UpdateBillboard()
    {
        _image.sprite = _spriteRingBuffer.Get();
        _slider.value = _spriteRingBuffer.GetAt();

        _goalManager.SetGoal(0, _spriteRingBuffer.GetAt() == _goalImageIndex);
    }
}
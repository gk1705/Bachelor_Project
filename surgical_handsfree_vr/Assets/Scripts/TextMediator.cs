using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMediator : MonoBehaviour
{
    [SerializeField] private List<TextObject> _texts;
    [SerializeField] private GameObject _board;
    [SerializeField] private Text _text;

    private Dictionary<string, string> _keyTextMappings;

    void Awake()
    {
        _keyTextMappings = new Dictionary<string, string>();

        foreach (var text in _texts)
        {
            _keyTextMappings.Add(text.Name, text.Text);
        }
    }

    public void SetText(string textKey)
    {
        if (_keyTextMappings.ContainsKey(textKey))
        {
            _text.text = _keyTextMappings[textKey];
        }
    }

    public void ToggleBoard()
    {
        _board.SetActive(!_board.activeSelf);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private List<Text> _goalTextFields;
    private string[] _goalTexts;
    private bool[] _goals;

    [SerializeField] private ChangeResponder _interactionChangeResponder;
    [SerializeField] private Text _heading;

    [SerializeField] private GoalTimer _goalTimer;

    void Awake()
    {
        _goals = new bool[_goalTextFields.Count];
        _goalTexts = new string[_goalTextFields.Count];
        for (int i = 0; i < _goalTextFields.Count; i++)
        {
            _goalTexts[i] = _goalTextFields[i].text;
        }
    }

    public void SetGoal(int index, bool value)
    {
        if (_goals[index] == value) return;

        _goals[index] = value;
        FormatGoalText(index);

        if (CheckGoalsReached())
        {
           _interactionChangeResponder.TriggerResponse("Congratulation!");
           _heading.text = "All goles have been fulfilled!";
            foreach (var goalTextField in _goalTextFields)
            {
                goalTextField.text = "";
            } 

            _goalTimer.LockTime();
        }
    }

    private bool CheckGoalsReached()
    {
        foreach (var goal in _goals)
        {
            if (!goal)
                return false;
        }

        return true;
    }

    private void FormatGoalText(int index)
    {
        if (_goals[index])
        {
            _goalTextFields[index].text = StrikeThrough(_goalTexts[index]);
            _goalTimer.LoggTime("Goal " + index.ToString() + " has been passed.");
        }
        else
        {
            _goalTextFields[index].text = _goalTexts[index];
            _goalTimer.LoggTime("Goal " + index.ToString() + " has been unpassed.");
        }
    }

    private string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }
}

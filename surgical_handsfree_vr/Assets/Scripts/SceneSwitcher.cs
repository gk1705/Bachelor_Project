using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private GoalTimer _goalTimer;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            _goalTimer.LockTime();
            SceneManager.LoadScene(sceneName: "TestingScene");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTimer : MonoBehaviour
{
    [SerializeField] private string _filePath;

    private float timePassed;
    private bool lockTimer;

    // Update is called once per frame
    void Update()
    {
        if (!lockTimer)
        {
            timePassed += Time.deltaTime;
        }
    }

    public void LockTime()
    {
        lockTimer = true;
        LoggTime("Scene time has been locked.");
    }

    public void LoggTime(string message)
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath);
        }

        using (StreamWriter file = new StreamWriter(_filePath, true))
        {
            file.WriteLine(SceneManager.GetActiveScene().name + " " + message + " Time passed: " + timePassed.ToString());
        }
    }
}

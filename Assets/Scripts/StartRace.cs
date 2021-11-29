using System;
using System.IO;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text timer;
    private bool startFlag = false;
    private float time = 0f;

    void FixedUpdate()
    {
        if(startFlag)
        {
            time += Time.fixedDeltaTime;
            timer.text = $"{time.ToString("n2")}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        startFlag = !startFlag;

        if(time > 0)
        {
            SaveResults();
        }

        time = 0f;
    }

    private void SaveResults()
    {
        Result resInst = new Result();
        resInst.Date = $"{DateTime.Now:dd-MM-yyyy}";
        resInst.ResultTime = $"{time}";

        string saveFileName = "Save";
        string newSave = $"{saveFileName}0";
        string savesDir = $"{Application.dataPath}/Saves/";
        for (int i = 0; File.Exists($"{savesDir}{newSave}"); i++)
        {
            newSave = $"{saveFileName}{i}";
        }
        Directory.CreateDirectory(savesDir);
        File.WriteAllLines($"{savesDir}{newSave}.save", new string[] { JsonUtility.ToJson(resInst, true) });
    }
}

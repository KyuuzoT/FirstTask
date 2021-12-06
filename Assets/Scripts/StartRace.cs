using System;
using System.IO;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text timer;
    private bool startFlag = false;
    private float time = 0f;
    private int filesIndex = 0;

    void FixedUpdate()
    {
        if (startFlag)
        {
            time += Time.fixedDeltaTime;
            timer.text = $"{time.ToString("n2")}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startFlag = !startFlag;

        if (time > 0)
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
        string savesDir = $"{Application.dataPath}/Saves/";

        Directory.CreateDirectory(savesDir);
        File.AppendAllLines($"{savesDir}{saveFileName}{filesIndex++}.save", new string[] { JsonUtility.ToJson(resInst, true) });
    }
}

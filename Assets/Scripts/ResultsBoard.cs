using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ResultsBoard : MonoBehaviour
{
    [SerializeField] private Transform Car;
    [SerializeField] private Transform MainUI;
    [SerializeField] private Transform ResultsBoardUI;
    [SerializeField] private Transform ResultsBoardUIGrid;
    [SerializeField] private Transform ResultRecord;
    private string savesDir;


    // Start is called before the first frame update
    void Awake()
    {
        savesDir = $"{Application.dataPath}/Saves";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainUI.gameObject.activeSelf)
            {
                MainUI.gameObject.SetActive(false);
                Car.gameObject.GetComponent<VehicleControl>().enabled = false;
                ResultsBoardUI.gameObject.SetActive(true);
                PrintResultsOnBoard(GetResults());
            }
            else
            {
                MainUI.gameObject.SetActive(true);
                ResultsBoardUI.gameObject.SetActive(false);
                Car.gameObject.GetComponent<VehicleControl>().enabled = true;
            }
        }
    }

    private List<Result> GetResults()
    {
        List<Result> resultsList = new List<Result>();
        Directory.GetFiles(savesDir);

        foreach (var saveFile in Directory.GetFiles(savesDir))
        {
            if (!saveFile.Contains(".meta"))
            {
                var recordsText = File.ReadAllText(saveFile).Replace("\r\n", "").Replace("\n","").Split('}');

                foreach (var rec in recordsText.Where(x => !string.IsNullOrEmpty(x)))
                {
                    var fromJson = JsonUtility.FromJson($"{rec}}}", typeof(Result)) as Result;
                    resultsList.Add(fromJson);
                }
            }
        }

        return resultsList;
    }

    private void PrintResultsOnBoard(List<Result> results)
    {
        var record = Instantiate(ResultRecord);

        results.ForEach(x =>
        {
            var record = Instantiate(ResultRecord);
            record.SetParent(ResultsBoardUIGrid);
            record.GetComponent<Text>().text = $"{x.Date}: {x.ResultTime} seconds";
        });
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsBoard : MonoBehaviour
{
    [SerializeField] private Transform Car;
    [SerializeField] private Transform MainUI;
    [SerializeField] private Transform ResultsBoardUI;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MainUI.gameObject.activeSelf)
            {
                MainUI.gameObject.SetActive(false);
                Car.gameObject.SetActive(false);
                ResultsBoardUI.gameObject.SetActive(true);
            }
            else
            {
                MainUI.gameObject.SetActive(true);
                ResultsBoardUI.gameObject.SetActive(false);
                Car.gameObject.SetActive(true);
            }
        }
    }
}

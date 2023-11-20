using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject EndOfGamePanel;


    private void Start()
    {
        //EndOfGamePanel.SetActive(true);
    }
    private void Update()
    {
        if (Health.isEnd)
        {
            EndOfGamePanel.SetActive(true);
        }
    }
}

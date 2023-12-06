using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class TextMeshGUIController : NetworkBehaviour
{

    ControllerPrototype controller1;
    ControllerPrototype controller2;

    public TextMeshProUGUI textMesh1;
    public TextMeshProUGUI textMesh2;

    public Health health;
    public Health health1;

    int counter;
    // Start is called before the first frame update
    private void Awake()
    {
      
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
   
    void Update()
    {

        /*     
        
        if (GameObject.FindWithTag("Player1") != null && GameObject.FindWithTag("Player2") != null)
        {
            
            if (counter < 1)
            {
                //health1 = GameObject.FindWithTag("Player2").transform.GetComponent<Health>();
                health = GameObject.FindWithTag("Player1").transform.GetComponent<Health>();
                health1 = GameObject.FindWithTag("Player2").transform.GetComponent<Health>();
            }

            textMesh1.text = health.NetworkedHealth.ToString();
            textMesh2.text = health1.NetworkedHealth.ToString();

        }

        //health1 = GameObject.FindWithTag("Player2").transform.GetComponent<Health>();
        health = GameObject.FindWithTag("Player1").transform.GetComponent<Health>();
        health1 = GameObject.FindWithTag("Player2").transform.GetComponent<Health>();

        */
    }
        

  } 

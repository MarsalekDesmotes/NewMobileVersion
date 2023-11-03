using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TickTimerTestEt : NetworkBehaviour
{

    [Networked] TickTimer timer { get; set; }


    public override void FixedUpdateNetwork()
    {

        //Debug.Log("Timer Expired");
        if (timer.ExpiredOrNotRunning(Runner))
        {

            timer = TickTimer.CreateFromSeconds(Runner, 0.5f);
            // Execute Logic

            // Reset timer
            timer = TickTimer.None;
            // alternatively: timer = default.

            Debug.Log("Timer Expired");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

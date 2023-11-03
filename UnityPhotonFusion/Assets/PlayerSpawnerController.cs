using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawnerController : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public static float degree;

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab , new Vector3(0,5,0), Quaternion.Euler(0,degree,0) ,player);
            degree = degree + 180;
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

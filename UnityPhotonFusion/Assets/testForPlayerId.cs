using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testForPlayerId : MonoBehaviour
{
    public static int playerId;

    public void onJoinRoom() //ikinci bilgisayarda önce 2 çalýþýyor
    {
        playerId++;
        Debug.Log("Oyuncu odaya girdi"+playerId);
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

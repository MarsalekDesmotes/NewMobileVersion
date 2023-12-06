using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public Quaternion quaternion;

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            //Sorunun sebebi bu LocalPlayer Kalkarsa 2.istemci 2 adet karkater üretiyor
            if(Runner.SessionInfo.PlayerCount == 1) //Ýlk gelen oyuncu oyuna girdi ve buradaki ayarlamalar gerçekleþti
            {
                quaternion.eulerAngles = new Vector3(0, 0, 0);
                /*PlayerPrefab.GetComponent<ControllerPrototype>().player = ControllerPrototype.playerSelector.player1;
                PlayerPrefab.tag = "Player1";
                PlayerPrefab.layer = 11;
                //PlayerPrefab.GetComponent<ControllerPrototype>().BallPrefab.gameObject.layer = 10;*/
                Runner.Spawn(PlayerPrefab, new Vector3(-7, 3, 0), quaternion, player); //Rotasyon deðerine göre tag ver         
            }
            if (Runner.SessionInfo.PlayerCount == 2) //Ýlk oyuncunun bilgisayarýnda ayarlar olmasý gerektiði gibi oldu fakat 2. oyuncunun bilgisayarýnda layer ayarlamasý olmadý yapýlmasý gereken characterController içinde spawned klasöründe eðer ben player counter = 1 iken girmiþsem benim layer ve player tag'im bu olsun kodunu yazmak
            {
                quaternion.eulerAngles = new Vector3(0, -180, 0);
                /*PlayerPrefab.GetComponent<ControllerPrototype>().player = ControllerPrototype.playerSelector.player2;
                PlayerPrefab.tag = "Player2";
                PlayerPrefab.layer = 12;
                //PlayerPrefab.GetComponent<ControllerPrototype>().BallPrefab.gameObject.layer = 10;*/
                Runner.Spawn(PlayerPrefab, new Vector3(7, 3, 0), quaternion, player);//Rotasyon deðerine göre tag ver       
            }
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

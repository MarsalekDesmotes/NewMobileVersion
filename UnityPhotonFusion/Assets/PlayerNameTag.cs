using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : NetworkBehaviour,IPlayerJoined
{
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("yeni oyuncu katıldı");
    }
    
}

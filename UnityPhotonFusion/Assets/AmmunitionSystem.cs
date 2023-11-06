using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class AmmunitionSystem : NetworkBehaviour
{

    [Networked(OnChanged = nameof(ChangedAmmunation))]
    public int Ammunation { get; set; } = 100; //Bunu top atýldýðýnda 1 yap 


    public override void Spawned()
    {
        Ammunation = 100; //Burada yazmak iþe yaradý 
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealRpc(int damage)
    {
        if (Ammunation > 0)
        {
  
            // The code inside here will run on the client which owns this object (has state and input authority).
            Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
            Ammunation -= damage; //çok hýzlý azalýyor counter koy
            Debug.Log("Mermi : " + Ammunation);
            //Explosion gibi effectler buraya konabilir. Önce test et eðer güzel çalýþýyorsa koy



            //Healt için onchange tanýmla ve hp barýný buraya entegre et 
        }

    }

    private static void ChangedAmmunation(Changed<AmmunitionSystem> changed)
    {
        Debug.Log("Mermi Sayisi Degisti");
    
    
    
    }



       
   

    
}

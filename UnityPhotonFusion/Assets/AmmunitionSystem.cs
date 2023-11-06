using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class AmmunitionSystem : NetworkBehaviour
{

    [Networked(OnChanged = nameof(ChangedAmmunation))]
    public int Ammunation { get; set; } = 100; //Bunu top at�ld���nda 1 yap 


    public override void Spawned()
    {
        Ammunation = 100; //Burada yazmak i�e yarad� 
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealRpc(int damage)
    {
        if (Ammunation > 0)
        {
  
            // The code inside here will run on the client which owns this object (has state and input authority).
            Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
            Ammunation -= damage; //�ok h�zl� azal�yor counter koy
            Debug.Log("Mermi : " + Ammunation);
            //Explosion gibi effectler buraya konabilir. �nce test et e�er g�zel �al���yorsa koy



            //Healt i�in onchange tan�mla ve hp bar�n� buraya entegre et 
        }

    }

    private static void ChangedAmmunation(Changed<AmmunitionSystem> changed)
    {
        Debug.Log("Mermi Sayisi Degisti");
    
    
    
    }



       
   

    
}

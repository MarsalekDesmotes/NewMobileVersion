using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;

    public int counter;
    public Animator characterAnim;



    private static void NetworkedHealthChanged(Changed<Health> changed)
    {
        Debug.Log("Calisti");
        /*
        if (changed.Behaviour.counter < 1)
        {
            changed.Behaviour.counter++;
            changed.Behaviour.characterAnim.SetTrigger("Hit");
        }
        */
        
        
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        if (NetworkedHealth > 0)
        {
            counter = 0;
            // The code inside here will run on the client which owns this object (has state and input authority).
            Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
            NetworkedHealth -= damage;
            //Explosion gibi effectler buraya konabilir. Önce test et eğer güzel çalışıyorsa koy

            

            //Healt için onchange tanımla ve hp barını buraya entegre et 
        }
        

    }

}


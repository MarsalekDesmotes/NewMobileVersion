using Fusion;
using UnityEngine;
using UnityEngine.UI;
public class Health : NetworkBehaviour
{
    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;

    public int counter;
    public Animator characterAnim;

    public bool MoveHp = false;

    public DamageIndıcator HpBar;


    private void Awake()
    {
        HpBar = GameObject.FindWithTag("HpBar").transform.GetComponent<DamageIndıcator>();
    }

    private static void NetworkedHealthChanged(Changed<Health> changed) //Hp bar burada güncellenmeli
    {
        Debug.Log("Calisti");
        /*
        if (changed.Behaviour.counter < 1)
        {
            changed.Behaviour.counter++;
            changed.Behaviour.characterAnim.SetTrigger("Hit");
        }
        */
        changed.Behaviour.MoveHp = true;
        changed.Behaviour.HpBar.DamageOkay();
        
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        //Bunun bir kez çalışmasını sağlayan sayaç 1 olduğunda hp bar harekete geçsin
        if (NetworkedHealth > 0)
        {
            counter = 0;
            // The code inside here will run on the client which owns this object (has state and input authority).
            Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
            NetworkedHealth -= damage;
            //Explosion gibi effectler buraya konabilir. Önce test et eğer güzel çalışıyorsa koy
            //MoveHp = true;
            

            //Healt için onchange tanımla ve hp barını buraya entegre et 
        }
        

    }

}


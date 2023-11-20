using Fusion;
using UnityEngine;
using UnityEngine.UI;
public class Health : NetworkBehaviour
{
    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 10;

    public int counter;

    public static bool isEnd;
    public int counterStart; 

    public Animator characterAnim;

    public bool MoveHp = false;

    public DamageIndıcator HpBar;
    public DamageIndıcator HpBar2;

    public GameObject EndOfMatch;

    int counter11;


    private void Start()
    {
        EndOfMatch = GameObject.FindWithTag("Orta");
    }
    public override void Spawned()
    {
        base.Spawned();
        isEnd = false;
        if(this.gameObject.tag == "Player1")
        {
            HpBar = GameObject.FindWithTag("HpBar").transform.GetComponent<DamageIndıcator>();   
        }
        if(this.gameObject.tag == "Player2")
        {
            HpBar = GameObject.FindWithTag("HpBar2").transform.GetComponent<DamageIndıcator>();
        }
        HpBar.characterHpDeterminer(NetworkedHealth); //HpBarına can sayısını gönderdiğimiz kısım 

    }
    private void Awake()
    {
         //Burada kaç canımız olduğunu belirledik bar ona göre azalsın diye, Sonradan özelleştirmek için fonksiyona karakterin gücü gibi parametreler eklenebii
    }

    private static void NetworkedHealthChanged(Changed<Health> changed) //Hp bar burada güncellenmeli
    {
        changed.Behaviour.counter11++;
        if (changed.Behaviour.counter11 > 1)
        {
            changed.Behaviour.HpBar.DamageOkay(); //Bu fonksiyon çalıştırıldığında can miktarında hp sayısına göre azalma olur.
            changed.Behaviour.counter11++;
        }
        
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage) //kaç vuracağımız burada
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
        else
        {
            isEnd = true;
            characterAnim.SetBool("FaintDown", true);
            //Panel Açılsın
            //EndOfMatch.SetActive(true);
        }
        

    }

}


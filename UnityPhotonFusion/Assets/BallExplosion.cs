
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using Spine.Unity;
using System.Collections;
using Fusion.Sockets;
using System.Collections.Generic;
using System;

public class BallExplosion : Fusion.NetworkBehaviour ,INetworkRunnerCallbacks
{


    //Buraya bir sayaç ekle ve o sayaç değeri 1'den büyükse topları isTrigger yap
    //isTrigger olan toplar toplanabilir statüdedir.
    //sayacı 1'den büyük olan toplar rakiple birden fazla kez temas edemez
    //Hatta rakiple temas etmesi bile şart değil herhangi bir yere çarpmış olması onun artık hasar vermeyen bir topa dönüşmesine neden olur.

    public int counter;

    public int counter2;

    [Networked(OnChanged = nameof(collidedOnSomething))]
    public bool BallCollided { get; set; }
    [Networked(OnChanged = nameof(ExplosionController))]
    public bool isCollided { get; set; }
    [Networked(OnChanged = nameof(ExplosionController2))]
    public bool isCollided2 { get; set; }


    int counterCollider;


    public NetworkObject ParticleEffect;

    private NetworkObject Ball;

    private Rigidbody2D rigidbody2D;

    private NetworkObject chracter1;

    private NetworkObject chracter2;

    public GameObject GameObject2;

    public float timeControl;

    public GameObject Player1;
    public GameObject Player2;

    public bool TopTemas;

    public int ammunation; //mermi değeri

    [Networked(OnChanged = nameof(CollectedSomething))]
    public bool isCollectible { get; set; }

    //Bunların değişimini
    public static bool isCollectibleBall; //Top eğer toplanabilirse o topla temasta ne bir particle ne de bir animasyon çalışmamalı(Top herhangi bir şeye çarptığında bu değer true olur ve topla temas sırasında particle effectler çalışmaz)
    //public static bool stopIt1; //Bunun networked ile tetiklenmesi veya networked bir değişken olması lazım
    //public static bool stopIt2; //Bunun networked ile tetiklenmesi veya networked bir değişken olması lazım
    private void Start()
    {
        //stopIt1 = false;
        //stopIt2 = false;
        Ball = GetComponent<NetworkObject>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        //timeControl = 0;
    }

    /*
    public override void FixedUpdateNetwork()
    {
        
        if (changed.Behaviour.isCollectible)
        {
            changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball); //herhangi bir yere 1 kere çarptıysa artık toplanabilir demektir. Herhangi bir yere çarpması sonucu top collectible oluyor zaten. Ama bu kod çalışmayacak çünkü onChange eventi sadece değişiklik olduğu zaman çalışır.
        }
    }
    */


    public static void collidedOnSomething(Changed<BallExplosion> changed)
    {
        //isCollectibleBall = true;
        //changed.Behaviour.isCollectible = true;
        changed.Behaviour.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //YERE ÇARPINCA DUR
        changed.Behaviour.gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    public static void CollectedSomething(Changed<BallExplosion> changed) //Mermi değerini Burada tut
    {
        changed.Behaviour.ammunation++; //her mermi toplandığında bu bir artacak     
    }

    //Bence hata 2 adet OnCollisionEnter kullanmak ikisini aynı yerde kullan
    //Yani hem canı aynı yerde azalt hem particle effecti aynı yerde patlat



    //Bunu ExplosionController1 ve 2 olarak ayır 
    public static void ExplosionController(Changed<BallExplosion> changed)
    {
        if (isCollectibleBall) //collect edilebilirse yok et 
        {
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball); //eğer daha önce herhangi bir yere çarptıysa toplanabilir bir obje halini almış demektir ve toplanabilir olmalıdır.
        }
        if (changed.Behaviour.counter < 1)
        {


            

            if (ControllerPrototype.characterHp1>0)
            {
                ControllerPrototype.characterHp1--;
            }
            
            changed.Behaviour.counter2++;
            //Burada geri gitme sorununa yoğunlaş
            changed.Behaviour.Runner.Spawn(changed.Behaviour.ParticleEffect, changed.Behaviour.gameObject.transform.position);
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball);
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball);
            //changed.Behaviour.rigidbody2D.simulated = false;
            changed.Behaviour.counter++;


            //changed.Behaviour.Player1.transform.GetComponent<Animator>().SetTrigger("Hit");
            
        }

    }


    public static void ExplosionController2(Changed<BallExplosion> changed)
    {
        if (isCollectibleBall) //collect edilebilirse yok e
        {
            //TOP TOPLAMA KISMI
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball); //eğer daha önce herhangi bir yere çarptıysa toplanabilir bir obje halini almış demektir ve toplanabilir olmalıdır.
        }
        if (changed.Behaviour.counter < 1)
        {

            if (ControllerPrototype.characterHp2 > 0)
            {
                ControllerPrototype.characterHp2--;
            }
            
            changed.Behaviour.counter2++;
            //Burada geri gitme sorununa yoğunlaş
            changed.Behaviour.Runner.Spawn(changed.Behaviour.ParticleEffect, changed.Behaviour.gameObject.transform.position);
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball);
            //changed.Behaviour.Runner.Despawn(changed.Behaviour.Ball);
            //changed.Behaviour.rigidbody2D.simulated = false;
            changed.Behaviour.counter++;


            //changed.Behaviour.Player2.transform.GetComponent<Animator>().SetTrigger("Hit");

        }

    }


    public override void Spawned()
    {
        isCollectibleBall = false; //Baslangicta top toplanabilir  olmamalı aksi taktirde

        isCollectible = false; //Top ilk spawn olduğunda hızlı bir şekilde rakibe doğru gidiyor olacak bu nedenle toplanabilir olmayacak.
        base.Spawned();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Burada topla ayrıca isGrounded olduğundan emin ol ve trigger stay kullan ve layerla ki fazla sıkıntı olmasın
    }
    //Distance hesaplayarak yapmayı da deneyebilirsin //Sanmıyorum çalışacağını
    private void OnCollisionEnter2D(Collision2D collision) //Stay olmazsa karakter topla tema
    {

        

        if(collision.gameObject.tag == "Ground")
        {
            
            //Burada onchange'de topu durdur
            if (BallCollided == true) //Top her ne ile temas ederse etsin top ile temasın yapıldığını detect eden bool değer true olacak 
            {
                BallCollided = false;
            }
            else
            {
               //Runner.Despawn OnChange
                BallCollided = true;
            }
            
            //isCollectible = true;
            //rigidbody2D.velocity = Vector2.zero; //Top bir terminalde durduğunda pozisyonu diğer terminallerde de tutulduğu için çalışacaktır diye düşünüyorum.
            //constraint kullan


            if(rigidbody2D != null)
            {
                //Runner.Despawn
                //rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX; //NullReferance  Bunu ball içine koyup sonra karkaterle temas sırasında çalışan onChange içerisinde kısıtlayabiliriz veya doğrudan constraint etmeyi deneyebiliriz.
            }
            
        }
        //Bu kısmı isTrigger ile yap ve karakterin önüne isTrigger olan bir collider koy
        //Ayrıca mermi toplamada sayılar düzgün çalışıyor mu diye bak 
        Debug.Log("Ammunation : " + ammunation);
        if (counter2 > 0)
        {
            isCollectible = true; //Bu değer sadece topu atan kişide collectible olur o nedenle bunun her iki karakter için de true olması için bunu bir onChange eventinin içinde true yapmalıyız.
        }
        

        if(rigidbody2D != null)
        {
            rigidbody2D.gravityScale = 1f; //Top herhangi bir yere çarparsa yer çekimi başlasın.
            
        }
              
        if (collision.gameObject.tag == "Player1")
        {
            if (HasStateAuthority == false) //En son burada değişiklik yaptım onun sebebi de otorite olmayan bilgisayarda buradaki can düşürme kodları çalışmasın diye 
            {
                return; 
            }

            if(collision.gameObject.TryGetComponent<Health>(out var health))
            {
                
                if (counterCollider < 1)
                {
                    health.DealDamageRpc(1);
                    //counter++; //Bu spawnlandığı için tekrar 0'lama gereği duymadım.
                    counterCollider++;
                }
                
            }

            //Player1 = collision.gameObject;


            //Buraya geri tepme eklenebilir hangi karakterle çarpıştığına göre 
            //stopIt1 = true;
            TopTemas = true;
         
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(50 * Runner.DeltaTime, 0, 0);
            //sonra yerle temas edince durdurmayı sağlayan kodu yaz 
            if (isCollided)
            {
                isCollided = false;
            }
            else
            {
                isCollided = true;
            }

        }

        if (collision.gameObject.tag == "Player2")
        {
            
            if (HasStateAuthority == false)
            {
                //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                return;
            }

            //Player2 = collision.gameObject;


            //Buraya geri tepme eklenebilir hangi karakterle çarpıştığına göre 
            //stopIt2 = true;
            TopTemas = true;
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-150 * Runner.DeltaTime, 2, 0); yanlış kullanım
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-50 * Runner.DeltaTime, 0, 0);
            if (isCollided2)
            {
                isCollided2 = false;
            }
            else
            {
                isCollided2 = true;
            }
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}

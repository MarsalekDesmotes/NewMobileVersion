
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using Spine.Unity;
using System.Collections;
using TMPro;
using Fusion.Sockets;
using System.Collections.Generic;
using System;

[ScriptHelp(BackColor = EditorHeaderBackColor.Steel)]
public class ControllerPrototype : Fusion.NetworkBehaviour , INetworkRunnerCallbacks {
  protected NetworkCharacterControllerPrototype _ncc;
  protected NetworkRigidbody _nrb;
  protected NetworkRigidbody2D _nrb2d;
  protected NetworkTransform _nt;


    public bool JumpControl; //havadayken true , yerde false 
    public TextMeshProUGUI text1; //Bunlar istenen formatta değil
    public TextMeshProUGUI text2;

    private bool outOfAmmunation;

    public float speed = 0.05f;
    public float jump;
    public NetworkRigidbody2D rigidbody;
    public static int PlayerCounter;

    int AmmunationCounter;

    public bool TestBoolOperation;

    [Networked(OnChanged = nameof(test))]
    public bool isGrdnd { get;set; }

    //public Button ShootButton;

    [Networked(OnChanged = nameof(HpBarController))]
    public bool characterHpTrigger { get; set; }

    
    public static int characterHp1 { get; set; } //Karakterin kalan can hakkını belirtmek için kullacağız. Yukarıda yer alan event sayesinde tetiklenecek.
    
    public static int characterHp2 { get; set; }

    int health;

    public bool inactivity; //true ise karaktere top çarpmıştır ve hareket etmemesi için inactivty durumuna geçirilmiştir.
    public bool inactivity2;

    //public static bool stopIt1;
    //public static bool stopIt2;

    public int ammunation;

    [Networked (OnChanged = nameof(JumpPlayer))]
    public bool isjumping { get; set; }

    [Networked (OnChanged = nameof(CollidedOnMe))]
    public bool isCollisionEnter { get; set; }

    [Networked(OnChanged = nameof(CollidedOnMe2))]
    public bool isCollisionEnter2 { get; set; }

    [Networked (OnChanged = nameof(AfterSpawn))]
    public bool AfterSpawnControl { get; set; }

    [Networked(OnChanged = nameof(AfterSpawn))]
    public bool AfterDeSpawnControl { get; set; }

    //[Networked(OnChanged = nameof(RemainingHealthChange))]
    //public int RemainingHealth { get; set; }

    [Networked] TickTimer Delay { get; set; }

    public float timer = 1.4f;

    public Rigidbody2D rigidbody2D;

    public Button forward;

    public Button back;

    public bool Test;

    public LayerMask Ground;
    NetworkObject Ball2;
    public static bool isGrounded1;
    public static bool isGrounded2;
    public SkeletonPartsRenderer skeletonParts;

    public NetworkObject BallPrefab;
    public Transform BallPosition;
    public SpriteRenderer BallSortingOrder;

    GroundColliderTest groundColliderTest;

    public bool isShot;

    public Vector3 velocity;

    public float GravityValue=-9.81f;

    public CharacterController _controller;

    public float JumpPower;

    public Animator anim;

    public Animator NetworkAnim;

    public int CounterOfJump; //Bununla tek seferlik geçiş sağlamaya çalış

    [Networked]
    public Vector3 MovementDirection { get; set; } //Networked anahtar kelimesi tüm bilgisayarlarda ortak değişmesi gereken parametreler için kullanılır.

    public bool TransformLocal = false;

    [DrawIf(nameof(ShowSpeed), Hide = true)]
    public float Speed = 6f;

    int counter;

    int counter3;

    UiButtonController Ui;

    public enum playerSelector
    {
        player1,
        player2,
    }


    public bool isntFirstShotComleted;

    public playerSelector player;

    bool ShowSpeed => this && !TryGetComponent<NetworkCharacterControllerPrototype>(out _);
 

    public static void UpdateHealthBar(Changed<ControllerPrototype> changed)
    {

    }

    public static void HpBarController(Changed<ControllerPrototype> changed)
    {
        
    }

    public static void JumpPlayer(Changed<ControllerPrototype> changed)
    {
        changed.Behaviour.JumpControl = true;
        changed.Behaviour.anim.SetBool("Jump", true);
        isGrounded1 = false;
        
    }

    public static void CollidedOnMe(Changed<ControllerPrototype> changed)
    {
        if (changed.Behaviour.TestBoolOperation && BallExplosion.isCollectibleBall == false) //Bunları explosion içine yazmayı düşün
        {
            changed.Behaviour.TestBoolOperation = false;


            //Constraint kullan ve zıplamasını engelle 

            changed.Behaviour.anim.SetTrigger("Hit");

           

            
            //changed.Behaviour.characterHp1--;
            //changed.Behaviour.RemainingHealth -= 10;
            //changed.Behaviour.text1.text = changed.Behaviour.ToString();
            if (characterHp1 == 0)
            {
                //ölme animasyonunu çalıştır
                changed.Behaviour.anim.SetBool("FaintDown", true);
            }
        }
    }
    public static void CollidedOnMe2(Changed<ControllerPrototype> changed)
    {
        if (changed.Behaviour.TestBoolOperation && BallExplosion.isCollectibleBall == false)
        {
            changed.Behaviour.TestBoolOperation = false;

            changed.Behaviour.anim.SetTrigger("Hit");

            //changed.Behaviour.characterHp2--;
            //changed.Behaviour.text2.text = changed.Behaviour.ToString();         
        }
        
    }


    public bool CoolDownController() //Cooldown süresi burada belirlenecek
    {
        if (isntFirstShotComleted)
        {
            isntFirstShotComleted = false;
            return true;
        }
        if (timer < 1.3f)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }


    public static void AfterSpawn(Changed<ControllerPrototype> changed)
    {
        if(changed.Behaviour.AfterDeSpawnControl == true)
        {
            Debug.Log("calıstııııı");
            changed.Behaviour.Ball2.GetComponentInChildren<Renderer>().enabled = false; 
            changed.Behaviour.BallSortingOrder.sortingOrder = 101; //Top elden çıktıktan sonra arkaya götür
            changed.Behaviour.AfterDeSpawnControl = false;
        }
        if(changed.Behaviour.AfterSpawnControl == true)
        {
            Debug.Log("aaaaaa");
            changed.Behaviour.Ball2.GetComponentInChildren<Renderer>().enabled = true; //
            changed.Behaviour.BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 94; //topu atarken öne getir
            changed.Behaviour.AfterSpawnControl = true;
        }
    }


    //Yukarıda onChange eventi tanımladık bu event içerisinde mevcut parametre değiştirildiğinde çalışacak fonksiyon bu olarak tanımlı
    public static void test(Changed<ControllerPrototype> changed)
    {
        Debug.Log("Test et");
        //Hata yok
        changed.Behaviour.anim.SetBool("Jump", false);
        //changed.Behaviour.anim.SetTrigger("Ground");
        changed.Behaviour.JumpControl = false; //karakterin havada durduğu sürenin sonuna geldiğimiz tespit edilir.
        changed.Behaviour.NetworkAnim = changed.Behaviour.anim;
        //changed.Behaviour.isGrdnd = false;
        
        //changed.Behaviour.anim.SetTrigger("Ground");
    }

  public void Awake()
  {
        //characterHp1 = 3;
        //characterHp2 = 3;
        //RemainingHealth = 100; //Bunu düşür
        //characterHp1 = health;
        Ui = GameObject.FindWithTag("UI").transform.GetComponent<UiButtonController>();
        //ShootButton = GameObject.FindWithTag("Shot").transform.GetComponent<Button>();



    CacheComponents();
    //_controller = GetComponent<CharacterController>();

  }


    public override void Spawned()
    {
        base.Spawned();
        outOfAmmunation = false;
        characterHp1 = 50;
        characterHp2 = 50;
        inactivity = false;
        ammunation = 10; //cephane
    }

    private void Start()
    {

        //Debug.Log("Oyuncu sayacı : "+ PlayerSpawnerPrototype.playerJoinCount);
        /*if (HasStateAuthority)
        {
            PlayerCounter++; //Burada her oyuncu girişinde burası bir artacak ve bir kere çalışacak(1 kere çalıştığı için ilk giren oyuncu etkilenmeyecek) böylelikle 2. oyuncunun girişini kontrol edebileceğiz
            Debug.Log("oyuncu sayısı : " + PlayerCounter);
            if (Runner.SessionInfo.PlayerCount > 1)
            {
                Debug.Log("oyuncu sayısı : " + PlayerCounter);
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            
        }
        */
        rigidbody2D = GetComponent<Rigidbody2D>();

    }


  

  private void CacheComponents() {
    if (!_ncc) _ncc     = GetComponent<NetworkCharacterControllerPrototype>();
    if (!_nrb) _nrb     = GetComponent<NetworkRigidbody>();
    if (!_nrb2d) _nrb2d = GetComponent<NetworkRigidbody2D>();
    if (!_nt) _nt       = GetComponent<NetworkTransform>();
    }


    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("GoBack", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("GoBack", false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Run", false);
        }
    }
    */

   

    private void Update()
    {
        //Debug.Log("Gravity Value : " + GravityValue);
    }


    //Karakterle top temas ettikten sonra burada tetiklenen bir bool değer karkakterin herhangi bir tuşla etkileşime girene kadar çalışmasını sağlayacak (aşağıda sağa sola gitme tuşlarının inputlarına basıldığında inactivity tekrar false olacak)
    //Yani karakter tuşlara basana kadar hareket etmemeye devam edecek tuşlara basıldıktan sonra tekrar bool değer false olacak ve hareket sağlanacak.
    //Çalışacak olan kod her terminal için özel olarak çalışacağından burada onChange event'i kullanımı gerekmiyor.





    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") //Bunu daha optimize yazmayı dene 
        {
            isGrounded1 = true; //Çalışmazsa bunları onChange ile tetikle
            //Burada isGrounded tekrar zıplamayı sağlatıyor fakat onChange ile animasyon tetiklenmesi gerektiği için JumpDown yine static olarak son karede bekleyişini sürdürüyor.

            //Hiç optimize gelmiyor. //Burası sürekli onChange'in çalışmasına sebep olacak bu nedenle kullanımı masraflı olabilir daha masrafsız bir çözüm bulmak iyi olacaktır.   
            
            if (isGrdnd == true)
            {
                isGrdnd = false;
            }
            else
            {
                isGrdnd = true;
            }
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Düşün : eğer inactivty false ise ...

        if (collision.gameObject.tag == "Ground")
        {
            //isGrounded = true; //Muhtemelen buraya da false ise true , ise false yapan bir sistem yazmalıyım //Sonrasında bu anlıko

            if(isGrdnd == true)
            {
                isGrdnd = false;
            }
            else
            {
                isGrdnd = true;
            }
        }
        
        if(collision.gameObject.tag == "Ball")
        {
            //Sorunun sebebi bu topa basarsa diye koymuştum

            //isGrdnd = true;

            //isGrounded = true;
        }

        
        if(collision.gameObject.tag == "Ball" && this.gameObject.tag == "Player1" && collision.transform.GetComponent<BallExplosion>().counter<1 && characterHp1>0) //Top 1 kez çarptıysa Player1'e
        {
            rigidbody2D.velocity = new Vector3(10000, 0, 0);

            inactivity = true; //Topla karakter çarpıştığı için inactivty = true olur 

            TestBoolOperation = true;

            if (isCollisionEnter)
            {
                isCollisionEnter = false;
            }
            else
            {
                isCollisionEnter = true;
            }

            //collision.gameObject.transform.GetComponent<BallExplosion>().isCollided = isCollisionEnter; //Yukaradaki değişiklikle bu objedeki değişiklikle tetiklenen ExplosionController fonksiyonu da çalışacak
        }
        if (collision.gameObject.tag == "Ball" && this.gameObject.tag == "Player2" && collision.transform.GetComponent<BallExplosion>().counter<1 && characterHp2>0) //Buraya networkHealth gelmeli
        {
            inactivity = true; // Topla karakter çarpıştığı için inactivty true olur ve karakterin belirlenen süre boyunca velocity değeri 0 olarak tutulur
            

            TestBoolOperation = true;

            if (isCollisionEnter2)
            {
                isCollisionEnter2 = false;
            }
            else
            {
                isCollisionEnter2 = true;
            }
            //collision.gameObject.transform.GetComponent<BallExplosion>().isCollided = isCollisionEnter2; //Yukaradaki değişiklikle bu objedeki değişiklikle tetiklenen ExplosionController fonksiyonu da çalışacak

        }


        


    }

    public override void FixedUpdateNetwork()
    {



        if (HasStateAuthority == false)
        {
            Debug.Log("eğer calisirsa aşağıda player1'in collider'ını yok et");
        }


        //if (HasStateAuthority == false) return;


        // Zemin ile temas halinde olup olmadığını kontrol etmek için bir küre oluştur
        //isGrounded = Physics.CheckSphere(transform.position, 0.5f, Ground);

        Debug.Log("Inactive : " + inactivity);




        if (player == playerSelector.player1)
        {
            if(HasStateAuthority == false)
            {
                return;
            }
            
            /*if (BallExplosion.stopIt1)
            {
                rigidbody2D.velocity = Vector3.zero;
                //rigidbody2D.isKinematic = true; //Bu işe yaradı
                //rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.None;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigidbody2D.isKinematic = false;
            }
            */

            if (Ui.isLeft) //Burada Ui awake kısmında yer alan Ui nesnesinden gelen değere göre buttona basılıp basılmadığı anlaşılır.
            {
                //inactivity = false; //yeni
                Debug.Log("GoBack");
                anim.SetBool("GoBack", true);
                transform.position += new Vector3(-speed * Runner.DeltaTime, 0, 0);
                //BallExplosion.stopIt1 = false;
                //rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            
            if (Ui.isLeft == false)
            {
                Debug.Log("GoBackFalse");
                anim.SetBool("GoBack", false);
            }


            if (Ui.right)
            {
                //inactivity = false; //yeni
                if (JumpControl) //işe yaramamış olabilir
                {
                    //eğer jump animsayonu çalışıyorsa koşma iptal olmalı
                    anim.SetBool("Run", false);
                }
                else
                {
                    anim.SetBool("Run", true);
                }
                
                transform.position += new Vector3(speed * Runner.DeltaTime, 0, 0);
                //BallExplosion.stopIt1 = false;
                //rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (Ui.right == false)
            {
                anim.SetBool("Run", false);
            }

            if (Ui.isJump == true && isGrounded1)
            {
                //rigidbody2D.constraints = RigidbodyConstraints2D.None;
                //BallExplosion.stopIt1 = false;
                //rigidbody2D.constraints = RigidbodyConstraints2D.None;
                if (isjumping)
                {
                    isjumping = false;
                }
                else
                {
                    isjumping = true;
                }

                //anim.SetBool("Jump", true);
                rigidbody2D.velocity = new Vector3(0, 600 * Runner.DeltaTime, 0);
                Ui.isJump = false;
                
                //isGrounded1 = false;
            }



            if (Ui.throwBall) //Timer başlangıçta 1.3'den büyük belirlense ilk basışta cooldown olmama şartı sağlanmış olur
            {
                              
                Ui.throwBall = false;

                if (Delay.ExpiredOrNotRunning(Runner))
                {                
                    Delay = TickTimer.CreateFromSeconds(Runner, 0.7f);
                    
                    Debug.Log("Test3");

                    if(outOfAmmunation == false) //mermi bittiyse animasyon çlaışmayacak ve atış animasyonla tetiklendiği için gerçekleşmeyecek
                    {
                        anim.SetBool("ThrowBall", true); //Bu hem havadakini hem yerdeki animasyon için geçerli
                    }
                                  
                    isShot = true;
                    //BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 101; //topu atarken öne getir

                    timer = 0; //Süre 1.3f'i geçtiğinde timer sonraki click için hazır olmalı
                    
                }


                if (CoolDownController()) //Bunu kaldırırsan hersey eski haline döner
                {
                    /*
                    Ui.throwBall = false;

                    anim.SetBool("ThrowBall", true);
                    isShot = true;
                    //BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 101; //topu atarken öne getir

                    timer = 0; //Süre 1.3f'i geçtiğinde timer sonraki click için hazır olmalı
                    */
                }
                


            }


            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("ThrowBall") || anim.GetCurrentAnimatorStateInfo(0).IsName("JumpThrowBall")))

            {
                if(gameObject.TryGetComponent<AmmunitionSystem>(out var ammunitionSystem))
                {
                    
                    if (Ui.AmmunationCounter < 1)
                    {
                        
                        ammunitionSystem.DealRpc(1);
                        Debug.Log("Ammunation Counter : " + AmmunationCounter);
                        //Ui.AmmunationCounter = ammunitionSystem.CounterOfDealRpc; 

                        Debug.Log("Buradasfsgsd : " + ammunitionSystem.Ammunation);
                        //Buraya bir bool ekle mermi 0 olduğu anda false olsun Adı da outOfAmmunation
                        Ui.AmmunationCounter = ammunitionSystem.Ammunation; //Doğrudan RPC'nin içerisinden local değişkene aktarım yapıyoruz
                        if(Ui.AmmunationCounter == 0)
                        {
                            outOfAmmunation = true;
                        }

                    }
                   
                }
                
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && isShot) //Burası localde hallolmalı bence çünkü iki atış arasında fark oluşabilir
                {
                    Debug.Log("AmmunationCounter: " + Ui.AmmunationCounter); //Bu mermi sayısıdır bu 0 olunca atış animasyonu da fırlatma mekaniği de çalışmaz
                    
                    isShot = false;
                    if (BallPosition.position.y < -1.4f)
                    {
                        BallPosition.position = new Vector3(BallPosition.position.x, -1.4f , BallPosition.position.z);
                        Ball2 = Runner.Spawn(BallPrefab , BallPosition.position , Quaternion.identity);
                    }
                    else
                    {
                        Ball2 = Runner.Spawn(BallPrefab, BallPosition.position, Quaternion.identity);
                    }
                    


                    if (Ball2 != null)
                    {

                        anim.SetBool("ThrowBall", false);

                        //Ball2.GetComponentInChildren<Renderer>().enabled = true;

                        //AfterSpawnControl = true;

                        Ball2.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(1000 * Runner.DeltaTime, 0, 0);

                        //BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 94; //Top elden çıktıktan sonra arkaya götür

                    }
                }
                
            }
            
            /*
            Debug.Log("Left: " + UiButtonController.isLeft);

            Debug.Log("Right: " + UiButtonController.right);

            if (UiButtonController.isLeft)
            {
                anim.SetBool("GoBack", true);
            }
            if (UiButtonController.right)
            {
                anim.SetBool("Run", true);
            }
            */

            transform.position += new Vector3(0, JumpPower, 0);

            /*
            if (_controller.isGrounded)
            {
                velocity = new Vector3(0, -1, 0);
            }
            */

            /*velocity.y += GravityValue * Runner.DeltaTime;

            if (Runner.Config.PhysicsEngine == NetworkProjectConfig.PhysicsEngines.None)
            {
                return;
            }*/
            /*
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("p");
            }
            */
        }


        //Bunların çalışmama sebebi imnput.getkey muhabberi ile beraber kullanılması




        

        //ayer2
        if(player == playerSelector.player2)
        {
            if (HasStateAuthority == false)
            {
                Debug.Log("Otorite değiilim");
                return;
            }

            if (characterHp2 == 0)
            {
                anim.SetBool("FaintDown", true);
            }
            /*
            if (inactivity)
            {
                Debug.Log("Test Ediliyor...");
                rigidbody2D.velocity = Vector3.zero;
            }
            */


            /*
            if (BallExplosion.stopIt2)
            {
                rigidbody2D.velocity = Vector3.zero;
                //rigidbody2D.isKinematic = true; //Bu işe yaradı
                //rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                //rigidbody2D.isKinematic = false; //Bu işe yaradı
                rigidbody2D.constraints = RigidbodyConstraints2D.None;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; //En son burada kaldık 
            }
            */

            if (Ui.isLeft) //Burada Ui awake kısmında yer alan Ui nesnesinden gelen değere göre buttona basılıp basılmadığı anlaşılır.
            {
                //BallExplosion.stopIt2 = false;
                anim.SetBool("Run", true);
                transform.position += new Vector3(-speed * Runner.DeltaTime, 0, 0);

            }


            if (Ui.isLeft == false)
            {
                
                anim.SetBool("Run", false);
            }


            if (Ui.right)
            {
                //BallExplosion.stopIt2 = false;  
                anim.SetBool("GoBack", true);
                transform.position += new Vector3(speed * Runner.DeltaTime, 0, 0);
            }

            if (Ui.right == false)
            {
                anim.SetBool("GoBack", false);
            }

            if (Ui.isJump == true && isGrounded1)
            {
                
                if (isjumping)
                {
                    isjumping = false;
                }
                else
                {
                    isjumping = true;
                }
                
                //rigidbody2D.constraints = RigidbodyConstraints2D.None;
                //BallExplosion.stopIt2 = false;
                //anim.SetBool("Jump", true); //Bu iki kez kullanılmamalı
                rigidbody2D.velocity = new Vector3(0, 600 * Runner.DeltaTime, 0);
                Ui.isJump = false;
                
            }


            if (Ui.throwBall)
            {

                Ui.throwBall = false;

                //BallExplosion.stopIt2 = false;

                if (Delay.ExpiredOrNotRunning(Runner))
                {
                    Delay = TickTimer.CreateFromSeconds(Runner, 0.7f);

                    Debug.Log("player 2 wasted");

                    //obje veya particle effect instantiate etmek için bu yöntem kullanılır. Mümkünse particle effect içerisinde süresi dolunca onu destroy veya setActive false yapan bir kod olsun.


                    if (outOfAmmunation == false) //mermi bittiyse animasyon çlaışmayacak ve atış animasyonla tetiklendiği için gerçekleşmeyecek
                    {
                        anim.SetBool("ThrowBall", true); //Bu hem havadakini hem yerdeki animasyon için geçerli
                    }

                    isShot = true;

                    timer = 0; //Bu niye var
                    

                }
                
                



                //Ball2 = Runner.Spawn(BallPrefab, BallPosition.position, Quaternion.identity); //instantiate yerıne kullanılacak
                //Ball2.GetComponentInChildren<Renderer>().enabled = false;

                //Ui.throwBall = false;

                //anim.SetBool("ThrowBall",true); //SetBool dene

                //BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 101; //topu atarken öne getir
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("ThrowBall") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && isShot) //Atış animasyonu başladı demektir 
            {
                Debug.Log("Atış Başladı");
            }

            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("ThrowBall") || anim.GetCurrentAnimatorStateInfo(0).IsName("JumpThrowBall")))

            {
                if (gameObject.TryGetComponent<AmmunitionSystem>(out var ammunitionSystem))
                {
                    if (Ui.AmmunationCounter < 1)
                    {

                        ammunitionSystem.DealRpc(1);

                        //Ui.AmmunationCounter = ammunitionSystem.CounterOfDealRpc; 

                        Debug.Log("Buradasfsgsd : " + ammunitionSystem.Ammunation);
                        //Buraya bir bool ekle mermi 0 olduğu anda false olsun Adı da outOfAmmunation
                        Ui.AmmunationCounter = ammunitionSystem.Ammunation; //Doğrudan RPC'nin içerisinden local değişkene aktarım yapıyoruz
                        if (Ui.AmmunationCounter == 0)
                        {
                            outOfAmmunation = true;
                        }

                    }

                }

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && isShot) //Burası localde hallolmalı bence çünkü iki atış arasında fark oluşabilir
                {
                    Debug.Log("AmmunationCounter: " + Ui.AmmunationCounter); //Bu mermi sayısıdır bu 0 olunca atış animasyonu da fırlatma mekaniği de çalışmaz

                    isShot = false;
                    if (BallPosition.position.y < -1.4f)
                    {
                        BallPosition.position = new Vector3(BallPosition.position.x, -1.4f, BallPosition.position.z);
                        Ball2 = Runner.Spawn(BallPrefab, BallPosition.position, Quaternion.identity);
                    }
                    else
                    {
                        Ball2 = Runner.Spawn(BallPrefab, BallPosition.position, Quaternion.identity);
                    }



                    if (Ball2 != null)
                    {

                        anim.SetBool("ThrowBall", false);

                        //Ball2.GetComponentInChildren<Renderer>().enabled = true;

                        //AfterSpawnControl = true;

                        Ball2.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(-1000 * Runner.DeltaTime, 0, 0);

                        //BallSortingOrder.transform.gameObject.GetComponent<Renderer>().sortingOrder = 94; //Top elden çıktıktan sonra arkaya götür

                    }
                }

            }
            /*
            Debug.Log("Left: " + UiButtonController.isLeft);

            Debug.Log("Right: " + UiButtonController.right);

            if (UiButtonController.isLeft)
            {
                anim.SetBool("GoBack", true);
            }
            if (UiButtonController.right)
            {
                anim.SetBool("Run", true);
            }
            */

            transform.position += new Vector3(0, JumpPower, 0);

            /*
            if (_controller.isGrounded)
            {
                velocity = new Vector3(0, -1, 0);
            }
            */

            /*velocity.y += GravityValue * Runner.DeltaTime;

            if (Runner.Config.PhysicsEngine == NetworkProjectConfig.PhysicsEngines.None)
            {
                return;
            }*/
            /*
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("p");
            }
            */
        }
        Vector3 direction;
        if (GetInput(out NetworkInputPrototype input))
        {

            direction = default;

            /*if (input.IsDown(NetworkInputPrototype.left))
            {
                //Debug.Log("shot tuşuna basıldı");
            }*/



            if (input.IsDown(NetworkInputPrototype.BUTTON_ACTION5))
            {
                Debug.Log("H tuşuna basıldı");
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_FORWARD))
            {
                direction += TransformLocal ? transform.forward : Vector3.forward;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_BACKWARD))
            {
                direction -= TransformLocal ? transform.forward : Vector3.forward;
            }

            if (input.IsDown(NetworkInputPrototype.leftButton))
            {
                transform.position += new Vector3(-speed * Runner.DeltaTime, 0, 0);
                //anim.SetBool("GoBack", true);
                Debug.Log("isleftTrue");

            }

            if (Ui.isLeft == false)
            {
                //anim.SetBool("GoBack", false); //Burası default olarak 
                Debug.Log("isLeftFalse");
                //anim.SetBool("GoBack", false);

            }

            if (Ui.isLeft == true)
            {
                anim.SetBool("GoBack", true);
            }



            /*if (input.IsDown(NetworkInputPrototype.BUTTON_JUMP))
            {   
                transform.position = new Vector2(0, jump * Runner.DeltaTime);
            }*/




            if (input.IsDown(NetworkInputPrototype.BUTTON_LEFT))
            {

                //direction -= TransformLocal ? transform.right : Vector3.right;                    
                transform.position += new Vector3(-speed * Runner.DeltaTime, 0, 0);
                //anim.SetBool("GoBack", true);          
                Test = true;

            }
            if (input.IsUp(NetworkInputPrototype.BUTTON_LEFT))
            {
                //anim.SetBool("GoBack", false);
                Test = false;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_RIGHT))
            {
                //direction += TransformLocal ? transform.right : Vector3.right;
                //transform.position += new Vector3(speed * Runner.DeltaTime, 0, 0);
                //anim.SetBool("Run", true);

            }
            if (input.IsUp(NetworkInputPrototype.BUTTON_RIGHT))
            {
                //anim.SetBool("Run", false);
            }

            direction = direction.normalized;

            MovementDirection = direction;

            if (input.IsDown(NetworkInputPrototype.BUTTON_JUMP))
            {




                if (_ncc)
                {
                    //_ncc.Jump();
                }
                else
                {
                    //direction += (TransformLocal ? transform.up : Vector3.up);
                }
            }
        }
        else
        {
            direction = MovementDirection;
        }

        if (_ncc)
        {
            _ncc.Move(direction);
        }
        else if (_nrb && !_nrb.Rigidbody.isKinematic)
        {
            _nrb.Rigidbody.AddForce(direction * Speed);
        }
        else if (_nrb2d && !_nrb2d.Rigidbody.isKinematic)
        {
            Vector2 direction2d = new Vector2(direction.x, direction.y + direction.z);
            _nrb2d.Rigidbody.AddForce(direction2d * Speed);
        }
        else
        {
            transform.position += (direction * Speed * Runner.DeltaTime);
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
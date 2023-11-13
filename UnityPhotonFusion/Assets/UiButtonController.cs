using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonController : MonoBehaviour //NetworkBehaviour'da yapılabilir
{
    //nesneyi startta isminden yakala ve çiz 
    public bool right;
    public bool isLeft;
    public bool isJump;
    public bool throwBall;
    public int counter;


    public int AmmunationCounter;


    public bool Test;

    public Button button;


    public bool isTimeFinished;

    //public Animator anim;

    //public static int counterShot;
    //public static int counterJump;

    public float timeControl;


    private void Start()
    {
        timeControl = 0;
    }
    private void Update()
    {
        if (throwBall)
        {
            Test = true;
            button.interactable = false;
        }
        if (Test&&timeControl<0.8f) //işlem localde gerçekleşeceği için mantıklı geldi
        {
            timeControl += Time.deltaTime;
        }
        else
        {
            timeControl = 0;
            button.interactable = true;
            Test = false;
            //Burada veya controller tarafında mermi bittiğinde bunun çalışmasını durduran birşey koy 
        }
    }

    public void RightDown() //Burada elini çekene kadar true kalıyor oysa bizim istediğimiz sadece tek seferlik olarak olması
    {
        right = true;        
    }


    public void RightUp()
    {
        //counterJump = 0;
        right = false;
    }
        
        
    public void LeftDown()
    {
        Debug.Log("shot tuşuna basıldı");
        isLeft = true;         
    }

    
    public void LeftUp()
    {
        Debug.Log("shot tuşuna basıldı");
        //counterShot = 0;
        isLeft = false;       
    }

    public void ThrowBall()
    {
        if (button.interactable) //ilk anda true 
        {
            Debug.Log("Firlattik");
            AmmunationCounter = 0;//Rpc'nin bir kere çağrılmasını sağlayan counter
            counter = 0;
            throwBall = true;
            isTimeFinished = false;
        }
        

        /*if (isTimeFinished)
        {
            counter = 0;
            throwBall = true;
            isTimeFinished = false;
        }*/
        
    }
    //Buttonun fonksiyonu olarak çağrılmalıdır event olarak up-down yapmak sakıncalıdır.
    //çünkü sayaç koyup tek sefer çalışması sağlanmalıdır.
    public void jumpDown() //Jump buttonuna basılmasını sembolize eder  
    {
        
        //Yere düşmeden zıplamasın diye bu kontrolü yapıyoruz
        if (ControllerPrototype.isGrounded1) //Bunu kaldır
        {
            //isJump = true;
            isJump = true;
            
        }
        
    }

    

    //public void  

}

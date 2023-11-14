using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class DamageEffect : NetworkBehaviour
{
    public ParticleSystem particle;

    public static bool isDamage;
    public Slider slider; //Slider value baþlangýçta bir deðeri alýr. Biz onu belirlenen public deðiþkene bölmeliyiz.

    public int hpValue; //Karakter kaç kez darbe alýnca yýkýlacak onu belirler. Bunu RPC fonksiyonundaki deðerden çekmeliyiz.
    float Timer = 0;

    public int decreasingValue; //Her çarmada lerp olarak azalacak olan miktaro

    public float TimeControl;

    public float test;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop(); //Efekt oyun baþýnda dursun diye var
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TimeControl : " + test);
        if (slider.value > 0.01f && isDamage && Timer < TimeControl) //Timer deðeri burada eksilmenin kaç saniye süreceðini belirliyor
        {
            Timer += Time.deltaTime; //Timer burada iþin kaç saniye süereceðinibelirlemek için var
            slider.value -= Time.deltaTime * speed;
        }
        else
        {
            particle.Stop(); //bar hareketi durduðunda efekt dursun diye //Buraya sayaç koyup tekrar DamageOkay olduðunda çalýþtýr ki sürekli stop func çalýþmasýn
            isDamage = false;
            Timer = 0;
        }
        
        

    }

    public void DamageOkay()
    {
        isDamage = true;
        particle.Play();
    }
}

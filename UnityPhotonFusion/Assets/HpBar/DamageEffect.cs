using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class DamageEffect : NetworkBehaviour
{
    public ParticleSystem particle;

    public static bool isDamage;
    public Slider slider; //Slider value ba�lang��ta bir de�eri al�r. Biz onu belirlenen public de�i�kene b�lmeliyiz.

    public int hpValue; //Karakter ka� kez darbe al�nca y�k�lacak onu belirler. Bunu RPC fonksiyonundaki de�erden �ekmeliyiz.
    float Timer = 0;

    public int decreasingValue; //Her �armada lerp olarak azalacak olan miktaro

    public float TimeControl;

    public float test;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop(); //Efekt oyun ba��nda dursun diye var
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TimeControl : " + test);
        if (slider.value > 0.01f && isDamage && Timer < TimeControl) //Timer de�eri burada eksilmenin ka� saniye s�rece�ini belirliyor
        {
            Timer += Time.deltaTime; //Timer burada i�in ka� saniye s�erece�inibelirlemek i�in var
            slider.value -= Time.deltaTime * speed;
        }
        else
        {
            particle.Stop(); //bar hareketi durdu�unda efekt dursun diye //Buraya saya� koyup tekrar DamageOkay oldu�unda �al��t�r ki s�rekli stop func �al��mas�n
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

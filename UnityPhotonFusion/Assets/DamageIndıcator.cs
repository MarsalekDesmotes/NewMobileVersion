using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndıcator : MonoBehaviour
{
    public ParticleSystem particle;

    public static bool isDamage;
    public Slider slider; //Slider value başlangıçta bir değeri alır. Biz onu belirlenen public değişkene bölmeliyiz.

    public int hpValue; //Karakter kaç kez darbe alınca yıkılacak onu belirler. Bunu RPC fonksiyonundaki değerden çekmeliyiz.
    float Timer = 0;

    public int decreasingValue; //Her çarmada lerp olarak azalacak olan miktaro

    public float TimeControl;

    public float test;

    public float deneme;
    
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop(); //Efekt oyun başında dursun diye var
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Denem : " + deneme);

        Debug.Log("TimeControl : " + test);

        if (slider.value > 0.01f && isDamage && Timer < 1.5f) //Timer değeri burada eksilmenin kaç saniye süreceğini belirliyor
        {
            Timer += Time.deltaTime; //Timer burada işin kaç saniye süereceğinibelirlemek için var
            slider.value -= Time.deltaTime * speed;
        }
        else
        {
            particle.Stop(); //bar hareketi durduğunda efekt dursun diye //Buraya sayaç koyup tekrar DamageOkay olduğunda çalıştır ki sürekli stop func çalışmasın
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndıcator : MonoBehaviour
{
    public ParticleSystem particle;

    public static bool isDamage;
    public Slider slider; //Slider value başlangıçta bir değeri alır. Biz onu belirlenen public değişkene bölmeliyiz.
    
    
    /**ÖNEMLİ**/
    public int hpValue; //Karakter kaç kez darbe alınca yıkılacak onu belirler. Bunu RPC fonksiyonundaki değere de vermeliyiz
    /**ÖNEMLİ**/

    float Timer = 0;

    public float extraHitDamage = 0; //eğer karakter geliştirildiğse normalden fazla vurabilir.

    public float decreasingValue; //Her çarmada lerp olarak azalacak olan miktar

    public float PowerUp = 1; //Karakterin özel güç gibi kullanımlarda X kat hasar vermesini sağlar

    public float tmpDecreasingValue;

    public float targetValue = 1f;

    public float deneme;
    
    public float speed;

    public GameObject Character1;
    

    public GameObject Character2;


    public void characterHpDeterminer(int hpValue)
    {
        decreasingValue = slider.value / (float)hpValue; //Burada hp barının kaç parçaya ayrılacağı yazıldı
        targetValue = slider.value; //Buradaysa canın tam değeri girildi (1.0) 
    }

    // Start is called before the first frame update
    void Start()
    {   
        particle.Stop(); //Efekt oyun başında dursun diye var
        //decreasingValue = slider.value / (float)hpValue; //Özel güç vs durumları için decrasingValue 2 ile çarpılabilir
        //tmpDecreasingValue = decreasingValue;
        //targetValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Denem : " + deneme);

        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * speed);

        //PowerUp özel bir durum olduğunda karakterin vuruş gücünü dinamik olarak artıracak olan değer 
       
        if (targetValue+0.01f > slider.value) //karkaterin hasar aldıktan sonra düşmesi gereken can değeri olan ; target değer ile slider değeri eşitlendiyse particle effect kapatılabilir
        {
            particle.Stop(); //bar hareketi durduğunda efekt dursun diye //Buraya sayaç koyup tekrar DamageOkay olduğunda çalıştır ki sürekli stop func çalışmasın
            isDamage = false;
            Timer = 0;
        }
        else
        {    
            
        }

        if(gameObject.TryGetComponent<AmmunitionSystem>(out var ammunitionSystem))
        {
            
        }
    }

    public void DamageOkay()
    {
        isDamage = true;
        particle.Play();
        //targetValue -= decreasingValue;
        targetValue -= extraHitDamage; //Kullanılması sağlıklı olmayabilir. PowerUp değeri yeterlidir.
        targetValue = Mathf.Max(PowerUp*targetValue - decreasingValue, 0); //PowerUp özel bir durum olduğunda vuruş hasarını katlaması içindir. extraHitDamage ise karakter geliştirildiği sırada kullanılır.
        //Bunu kullanmamım bir sebebi ise ard arda alınan damage'larda karakterin canının azalmaya devam edip gerçek değere kadar çalışacak bir sistemde yazmak istememdir.

    }
}

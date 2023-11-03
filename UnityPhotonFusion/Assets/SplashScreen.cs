using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SplashScreen : MonoBehaviour
{
    // Yükleme ekranı için UI elemanları
    public GameObject loadingScreen;
    public Slider progressBar;
    public TMP_Text progressText;
    public TMP_Text continueText;

    // Yükleme işlemi için AsyncOperation nesnesi
    private AsyncOperation async;

    // Yükleme işleminin tamamlanıp tamamlanmadığını tutan değişken
    private bool isDone = false;

    // Başlangıçta çalışacak metod
    void Start()
    {
        // Yükleme ekranını aktif et
        loadingScreen.SetActive(true);

        // Asenkron olarak boş sahneyi yükle
        StartCoroutine(LoadEmptyScene());
    }

    // Her karede çalışacak metod
    void Update()
    {
        // Yükleme işlemi tamamlanmışsa
        if (isDone)
        {
            // Devam etmek için boşluk tuşuna basın yazısını göster
            continueText.gameObject.SetActive(true);

            // Boşluk tuşuna basılırsa
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Sahneyi aktif et
                async.allowSceneActivation = true;
            }
        }
    }

    // Asenkron olarak boş sahneyi yükleyen metod
    IEnumerator LoadEmptyScene()
    {
        // SceneManager.LoadSceneAsync metodunu çağır ve AsyncOperation nesnesini al
        async = SceneManager.LoadSceneAsync("Empty", LoadSceneMode.Single);

        // Sahnenin aktif olmasını engelle
        async.allowSceneActivation = false;

        // Yükleme işlemi bitene kadar döngüde kal
        while (!async.isDone)
        {
            // Yükleme ilerlemesini al (0 ile 0.9 arasında değer döner)
            float progress = Mathf.Clamp01(async.progress / 0.9f);

            // Yükleme ilerlemesini UI elemanlarına yansıt
            progressBar.value = progress;
            progressText.text = progress * 100f + "%";

            // Yükleme ilerlemesi 0.9'a ulaştıysa
            if (async.progress == 0.9f)
            {
                // Tamamlandı değişkenini true yap
                isDone = true;
            }

            // Bir sonraki kareye kadar bekle
            yield return null;
        }
    }
}

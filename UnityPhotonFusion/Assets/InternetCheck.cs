using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InternetCheck : MonoBehaviour
{
    // Uyarı mesajı için UI elemanları
    public GameObject warningPanel;
    public TMP_Text warningText;
    public Button warningButton;

    // Başlangıçta çalışacak metod
    void Start()
    {
        // Uyarı panelini pasif et
        warningPanel.SetActive(false);

        // Uyarı butonuna tıklandığında çalışacak metodu belirle
        warningButton.onClick.AddListener(CloseWarning);
    }

    // Her karede çalışacak metod
    void Update()
    {
        // Kullanıcının internete bağlı olup olmadığını kontrol et
        CheckInternetConnection();
    }

    // Kullanıcının internete bağlı olup olmadığını kontrol eden metod
    void CheckInternetConnection()
    {
        // Cihaz internete bağlı değilse
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // Uyarı panelini aktif et
            warningPanel.SetActive(true);

            // Uyarı metnini ayarla
            warningText.text = "İnternete bağlı değilsiniz. Lütfen internet bağlantınızı kontrol edin.";
        }
        else
        {
            // Uyarı panelini pasif et
            warningPanel.SetActive(false);
        }
    }

    // Uyarı butonuna tıklandığında çalışacak metod
    void CloseWarning()
    {
        // Uyarı panelini pasif et
        warningPanel.SetActive(false);
    }
}

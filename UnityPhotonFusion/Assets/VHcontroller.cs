using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHcontroller : MonoBehaviour
{
    public RectTransform canvas;

    [Header("Iphone12")]
    public Vector2 Iphone12; 



    public enum SelectionPhone
    {
        [Tooltip("iPhone 12")]
        Iphone12,

        [Tooltip("iPhone 11")]
        Iphone11
    }

    public SelectionPhone select;

    // Start is called before the first frame update
    void Start()
    {
        //if(Screen.currentResolution == 1222)
        if(Screen.currentResolution.width == Iphone12.x && Screen.currentResolution.height == Iphone12.y) 
        {
            canvas.sizeDelta = Iphone12; //Burada canvas doğru şekilde konumlandırılacak 


        }


        
        if(select == SelectionPhone.Iphone11)
        {
            //Iphone çözünürlük değerleri
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

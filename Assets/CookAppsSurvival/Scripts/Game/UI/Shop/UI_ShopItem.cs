using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class UI_ShopItem : MonoBehaviour
    {        
        [SerializeField] private Image image_Face;
        [SerializeField] private TextMeshProUGUI text_Desc;
        [SerializeField] private TextMeshProUGUI text_Price;
        [SerializeField] private Button button_Buy;

        internal void Initialize()
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public abstract class UI_ShopItem : MonoBehaviour
    {
        [SerializeField] private Image image_Face;
        [SerializeField] private TextMeshProUGUI text_Desc;
        [SerializeField] private TextMeshProUGUI text_Price;
        [SerializeField] private Button button_Buy;

        protected GoldSystem _goldSystem;
        protected int price;

        internal void Initialize(Sprite sprite, string desc, int price)
        {
            button_Buy.onClick.AddListener(BuyItem);

            image_Face.sprite = sprite;
            text_Desc.text = desc;
            text_Price.text = price.ToString();
            this.price = price;

            _goldSystem = BattleManager.Instance.GetSubSystem<GoldSystem>();
        }

        protected abstract void BuyItem();
    }
}

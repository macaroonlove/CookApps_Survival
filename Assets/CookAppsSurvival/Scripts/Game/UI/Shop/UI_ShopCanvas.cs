using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class UI_ShopCanvas : MonoBehaviour
    {
        void Awake()
        {
            List<UI_ShopItem> items = new List<UI_ShopItem>();
            items.AddRange(GetComponentsInChildren<UI_ShopItem>());

            foreach (var item in items)
            {
                item.Initialize();
            }
        }
    }
}

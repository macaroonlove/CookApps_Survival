using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class UI_ShopCanvas : MonoBehaviour
    {
        List<UI_ShopItem> items = new List<UI_ShopItem>();

        private bool isFirst = true;

        void Awake()
        {
            items.AddRange(GetComponentsInChildren<UI_ShopItem>());
        }

        public void Show()
        {
            if (isFirst)
            {
                isFirst = !isFirst;

                foreach (var item in items)
                {
                    if (item is UI_ShopBuffItem buffItem)
                    {
                        buffItem.Initialize();
                    }
                    else if (item is UI_ShopHealItem healItem)
                    {
                        healItem.Initialize();
                    }
                    else if (item is UI_ShopSkillUpgradeItem skillUpgradeItem)
                    {
                        skillUpgradeItem.Initialize();
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Shop/HealItem", fileName = "HealItem", order = 0)]
    public class HealItemTemplate : ScriptableObject
    {
        public Sprite face;
        public string desc;
        public int price;

        public float healPer;
    }
}
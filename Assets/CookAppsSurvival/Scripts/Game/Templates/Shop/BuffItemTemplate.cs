using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Shop/BuffItem", fileName = "BuffItem", order = 0)]
    public class BuffItemTemplate : ScriptableObject
    {
        public Sprite face;
        public string desc;
        public int price;

        public BuffTemplate template;

        public FX fx;
    }
}

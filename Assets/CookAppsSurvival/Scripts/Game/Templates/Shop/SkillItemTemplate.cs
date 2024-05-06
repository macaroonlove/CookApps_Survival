using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Shop/SkillItem", fileName = "SkillItem", order = 0)]
    public class SkillItemTemplate : ScriptableObject
    {
        public Sprite face;
        public string desc;
        public int price;
        public EJob job;
    }
}
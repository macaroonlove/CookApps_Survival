using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/AgentSkill", fileName = "Skill", order = 0)]
    public class AgentSkillTemplate : ScriptableObject
    {
        [Header("기본정보")]
        public string displayName;
        [TextArea]
        public string description;

        [Space(10)]
        public float cooldownTime;

        [Header("리소스 관련")]
        public Sprite face;        

        [Header("스킬 구현")]
        public List<SkillEffect> effects;
    }
}

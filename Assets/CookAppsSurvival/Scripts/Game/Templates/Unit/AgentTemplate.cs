using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 아군 유닛 템플릿
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Agent", fileName = "Agent", order = 0)]
    public class AgentTemplate : ScriptableObject
    {
        public int id;

        [Header("기본 정보")]
        public string displayName;
        public EJob job;

        [Header("레벨 정보")]
        [ReadOnly] public int Level;
        [ReadOnly] public int EXP;

        [Header("리소스 정보")]
        public Sprite face;
        public GameObject prefab;
        
        [Header("전투 관련")]
        public int maxHp;
        public int ATK;
        //public int DEF;

        [Space(10)]
        public float attackTerm;
        public float attackRange;

        [Header("이동 관련")]
        public float moveSpeed;

        [Header("스킬")]
        public bool isUpgade;
        public AgentSkillTemplate skillTemplate;
        public AgentSkillTemplate skillTemplate_Upgrade;
    }
}
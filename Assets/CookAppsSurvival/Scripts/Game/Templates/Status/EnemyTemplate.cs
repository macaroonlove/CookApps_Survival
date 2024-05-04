using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 적 유닛 템플릿
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Enemy", fileName = "Enemy", order = 0)]
    public class EnemyTemplate : ScriptableObject
    {
        public int id;

        [Header("기본 정보")]
        public string displayName;

        public EEnemyType enemyType;
        
        [Space(10)]
        public GameObject prefab;

        [Header("전투 관련")]
        public int maxHp;
        public int ATK;
        //public int DEF;

        public float attackTerm;
        public float attackRange;
    }
}
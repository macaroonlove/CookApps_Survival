using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public enum EJob
    {
        Tanker,
        Melee,
        Archer,
        Priest,
    }

    public enum EEnemyType
    {
        Normal,
        Boss,
    }

    public enum EPercentageType
    {
        /// <summary>
        /// 공격력의 n%
        /// </summary>
        ATK,
        /// <summary>
        /// 최대체력의 n%
        /// </summary>
        MaxHP,
    }

    public enum EEnemyTarget
    {
        /// <summary>
        /// 기존 적군
        /// </summary>
        ExistingEnemy,
        /// <summary>
        /// 범위 내 적군 하나
        /// </summary>
        OneEnemyInRange,
        /// <summary>
        /// 범위 내 적군 (수)만큼
        /// </summary>
        NumEnemyInRange,
        /// <summary>
        /// 범위 내 적군 모두
        /// </summary>
        AllEnemyInRange,
        /// <summary>
        /// 모든 적군
        /// </summary>
        AllEnemy,
    }

    public enum EAgentTarget
    {
        /// <summary>
        /// 자기 자신
        /// </summary>
        Myself,
        /// <summary>
        /// 범위 내 아군 하나
        /// </summary>
        OneAgentInRange,
        /// <summary>
        /// 범위 내 아군 모두
        /// </summary>
        AllAgentInRange,
        /// <summary>
        /// 자신을 제외한 범위 내 아군 모두
        /// </summary>
        AllAgentInRangeExceptMe,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 레벨업 테이블
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/LevelUpCurve", fileName = "LevelUpCurve", order = 0)]
    public class LevelUpCurveTemplate : ScriptableObject
    {
        [Header("애니메이션 커브")]
        public AnimationCurve needExp;
    }
}
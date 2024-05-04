using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 파티원 부활에 필요한 요소 템플릿
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/PartyRevival", fileName = "PartyRevival", order = 0)]
    public class PartyRevivalTemplate : ScriptableObject
    {
        [Header("부활 시간")]
        [Range(0.0f, 100.0f)]
        public float respawnTime = 5;
    }
}
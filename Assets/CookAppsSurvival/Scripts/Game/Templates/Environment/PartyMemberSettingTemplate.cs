using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 파티 설정
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/PartySetting", fileName = "PartySetting", order = 0)]
    public class PartySettingTemplate : ScriptableObject
    {
        [Header("파티멤버 ")]
        public List<AgentTemplate> partyMembers = new List<AgentTemplate>();

        [Header("유닛 부활 대기시간")]
        [Range(0.0f, 100.0f)]
        public float respawnTime = 5;

        [Header("사망 애니메이션 대기시간")]
        [Range(0.0f, 100.0f)]
        public float deathAnimTime = 1;
    }
}
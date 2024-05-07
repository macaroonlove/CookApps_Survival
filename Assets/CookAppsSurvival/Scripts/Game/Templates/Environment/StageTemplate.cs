using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/Stage", fileName = "Stage", order = 0)]
    public class StageTemplate : ScriptableObject
    {
        [Header("씬")]
        public string sceneName;

        [Header("적 유닛")]
        public EnemyTemplate normalEnemy;
        public EnemyTemplate bossEnemy;

        [Header("보스까지 처치해야 할 적의 수")]
        public int killUntilBoss;

        [Header("파티 설정")]
        public PartySettingTemplate partySettingTemplate;

        [Header("스폰 설정")]
        public EnemySpawnTemplate spawnTemplate;
    }
}
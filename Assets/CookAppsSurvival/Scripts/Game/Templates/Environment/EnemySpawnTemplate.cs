using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 몬스터를 생성하는데 필요한 요소 템플릿
    /// </summary>
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/EnemySpawn", fileName = "EnemySpawn", order = 0)]
    public class EnemySpawnTemplate : ScriptableObject
    {
        [Header("생성 주기")]
        [Range(0.0f, 100.0f)]
        public float spawnTime = 5;
        
        [Header("동시 생성 가능한 최대 수")]
        [Range(1, 100)]
        public int simultaneousMaxCnt = 5;

        [Header("몬스터가 나타날 위치(플레이어 + 반지름)")]
        [Range(1.0f, 200.0f)]
        public float spawnRadius = 1;
        
        [Header("몬스터 풀의 사이즈")]
        [Range(1, 500)]
        public int poolSize = 10;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// 적을 스폰하는 시스템
    /// </summary>
    public class SpawnSystem : MonoBehaviour, ISubSystem
    {
        private Stack<EnemyUnit> _enemyUnits;

        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private Coroutine coroutine;
        private WaitForSeconds waitForSeconds;
        
        private int _simultaneousMaxCnt;
        private float _spawnRadius;
        private int _size;

        internal event UnityAction<PartyUnit> onCompleteSpawn;

        public void Initialize(StageTemplate stage)
        {
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            waitForSeconds = new WaitForSeconds(stage.spawnTemplate.spawnTime);

            _simultaneousMaxCnt = stage.spawnTemplate.simultaneousMaxCnt;
            _spawnRadius = stage.spawnTemplate.spawnRadius;
            _size = stage.spawnTemplate.poolSize;
            
            InitPool(stage.normalEnemy);
            coroutine = StartCoroutine(AutomaticSpawn(stage.normalEnemy));
        }

        public void Deinitialize()
        {
            foreach (var unit in _enemyUnits)
            {
                Destroy(unit.gameObject);
            }
            _enemyUnits.Clear();
        }

        private void InitPool(EnemyTemplate template)
        {
            _enemyUnits = new Stack<EnemyUnit>();
            
            for (int i = 0; i < _size; i++)
            {
                EnemyUnit enemyUnit = Instantiate(template.prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
                enemyUnit.gameObject.SetActive(false);
                _enemyUnits.Push(enemyUnit);
            }
        }

        private IEnumerator AutomaticSpawn(EnemyTemplate template)
        {
            while (true)
            {
                int spawnCnt = Random.Range(1, _simultaneousMaxCnt);

                for (int i = 0; i < spawnCnt; i++)
                {
                    SpawnEnemy(template, _partySystem.mainUnit.transform.position);
                }

                yield return waitForSeconds;
            }
        }

        /// <summary>
        /// 플레이어 위치를 기준으로 원을 그려 랜덤 위치 받아오기
        /// </summary>
        private Vector3 GetRandomPos(Vector3 partyPos, float newSpawnRadius = 0)
        {
            float spawnRadius = _spawnRadius;

            if (newSpawnRadius != 0)
            {
                spawnRadius = newSpawnRadius;
            }

            Vector3 randomOffset = Random.onUnitSphere * spawnRadius;
            randomOffset.y = 1f;

            Vector3 spawnPosition = partyPos + randomOffset;

            return spawnPosition;
        }

        internal EnemyUnit SpawnEnemy(EnemyTemplate template, Vector3 partyPos)
        {
            EnemyUnit enemyUnit;
            if (template.enemyType == EEnemyType.Boss || _enemyUnits.Count == 0)
            {
                enemyUnit = Instantiate(template.prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
            }
            else
            {
                enemyUnit = _enemyUnits.Pop();
                enemyUnit.gameObject.SetActive(true);
            }
            var spawnPos = GetRandomPos(partyPos);
            enemyUnit.transform.position = spawnPos;
            
            enemyUnit.Initialize(template);

            _enemySystem.Add(enemyUnit);

            onCompleteSpawn?.Invoke(_partySystem.mainUnit);

            return enemyUnit;
        }

        public void DespawnEnemy(EnemyUnit enemyUnit)
        {
            enemyUnit.gameObject.SetActive(false);
            _enemyUnits.Push(enemyUnit);
            _enemySystem.Remove(enemyUnit);
        }

        internal void DisableAllEnemy()
        {
            // 자동 생성 멈추기
            StopCoroutine(coroutine);

            var enemies = new List<EnemyUnit>(_enemySystem.AllEnemies());

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].template.enemyType == EEnemyType.Boss) continue;

                if (enemies[i].healthAbility.IsAlive)
                {
                    DespawnEnemy(enemies[i]);
                }
            }
        }
    }
}

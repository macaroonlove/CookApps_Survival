using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 적을 스폰하는 시스템
    /// </summary>
    public class SpawnSystem : MonoBehaviour, ISubSystem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private EnemySpawnTemplate template;

        private Stack<EnemyUnit> _enemyUnits;

        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private WaitForSeconds waitForSeconds;

        private int _simultaneousMaxCnt = 1;
        private float _spawnRadius = 1;
        private int _size = 10;

        public void Initialize()
        {
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            waitForSeconds = new WaitForSeconds(template.cycleTime);
            _simultaneousMaxCnt = template.simultaneousMaxCnt;
            _spawnRadius = template.spawnRadius;
            _size = template.poolSize;

            InitPool();
            StartCoroutine(AutomaticSpawn());
        }

        public void Deinitialize()
        {
            foreach (var unit in _enemyUnits)
            {
                Destroy(unit.gameObject);
            }
            _enemyUnits.Clear();
        }

        private void InitPool()
        {
            _enemyUnits = new Stack<EnemyUnit>();
            
            for (int i = 0; i < _size; i++)
            {
                EnemyUnit enemyUnit = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
                enemyUnit.gameObject.SetActive(false);
                _enemyUnits.Push(enemyUnit);
            }
        }

        private IEnumerator AutomaticSpawn()
        {
            while (true)
            {
                int spawnCnt = Random.Range(1, _simultaneousMaxCnt);

                for (int i = 0; i < spawnCnt; i++)
                {
                    SpawnEnemy(_partySystem.pos.position);
                }

                yield return waitForSeconds;
            }
        }

        /// <summary>
        /// 플레이어 위치를 기준으로 원을 그려 랜덤 위치 받아오기
        /// </summary>
        private Vector3 GetRandomPos(Vector3 partyPos)
        {
            Vector3 randomOffset = Random.onUnitSphere * _spawnRadius;
            randomOffset.y = 1f;

            Vector3 spawnPosition = partyPos + randomOffset;

            return spawnPosition;
        }

        private void SpawnEnemy(Vector3 partyPos)
        {
            EnemyUnit enemyUnit;
            if (_enemyUnits.Count == 0)
            {
                enemyUnit = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
                //enemyUnit.gameObject.SetActive(false);
                //_enemyUnits.Push(enemyUnit);
            }
            else
            {
                enemyUnit = _enemyUnits.Pop();
                enemyUnit.gameObject.SetActive(true);
            }
            var spawnPos = GetRandomPos(partyPos);
            enemyUnit.transform.position = spawnPos;
            enemyUnit.Initialize();

            _enemySystem.Add(enemyUnit);
        }

        public void DespawnEnemy(EnemyUnit enemyUnit)
        {
            enemyUnit.gameObject.SetActive(false);
            _enemyUnits.Push(enemyUnit);
            _enemySystem.Remove(enemyUnit);
        }
    }
}

using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// 적의 위치를 받아오거나 범위 내에 존재하는지 등을 알 수 있는 클래스
    /// </summary>
    public class EnemySystem : MonoBehaviour, ISubSystem
    {
        [SerializeField, ReadOnly] private List<EnemyUnit> _enemies = new List<EnemyUnit>();

        internal event UnityAction<EnemyUnit> onDieEnemy;

        public void Initialize(StageTemplate stage)
        {
            
        }

        public void Deinitialize()
        {
            foreach (var item in _enemies)
            {
                Destroy(item.gameObject);
            }
            _enemies.Clear();
        }

        internal void Add(EnemyUnit instance)
        {
            _enemies.Add(instance);
        }

        internal void Remove(EnemyUnit instance)
        {
            _enemies.Remove(instance);

            onDieEnemy?.Invoke(instance);
        }

        #region 유틸리티 메서드
        /// <summary>
        /// 모든 적 반환
        /// </summary>
        internal List<EnemyUnit> AllEnemies()
        {
            return _enemies;
        }

        /// <summary>
        /// 자신을 기준으로 가장 가까운 적을 반환
        /// </summary>
        /// <returns></returns>
        internal EnemyUnit FindNearestEnemy(Vector3 unitPos)
        {
            EnemyUnit nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distance = Vector3.Distance(enemy.transform.position, unitPos);
                    if (distance < nearestDistance)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = distance;
                    }
                }
            }

            return nearestEnemy;
        }

        /// <summary>
        /// 자신을 기준으로 범위 내에 가장 가까운 적을 반환
        /// </summary>
        internal EnemyUnit FindNearestEnemyInRange(Vector3 unitPos, float radius)
        {
            EnemyUnit nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distance = Vector3.Distance(enemy.transform.position, unitPos);
                    if (distance < nearestDistance && distance <= radius)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = distance;
                    }
                }
            }

            return nearestEnemy;
        }

        /// <summary>
        /// 범위 내 적 찾기
        /// </summary>
        internal List<EnemyUnit> FindEnemiesInRadius(Vector3 unitPos, float radius, int maxCount = int.MaxValue)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemies.Count >= maxCount) break;

                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    if (Vector3.Distance(enemy.transform.position, unitPos) <= radius)
                    {
                        if (enemies.Contains(enemy) == false)
                        {
                            enemies.Add(enemy);
                        }
                    }
                }
            }

            return enemies;
        }
        #endregion
    }
}

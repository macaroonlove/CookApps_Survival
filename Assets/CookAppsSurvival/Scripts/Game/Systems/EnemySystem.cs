using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 적의 위치를 받아오거나 범위 내에 존재하는지 등을 알 수 있는 클래스
    /// </summary>
    public class EnemySystem : MonoBehaviour, ISubSystem
    {
        [SerializeField, ReadOnly] private List<EnemyUnit> _enemies = new List<EnemyUnit>();

        public void Initialize()
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
        }

        /// <summary>
        /// 메인 유닛으로부터 가장 가까운 적을 반환
        /// </summary>
        /// <returns></returns>
        internal EnemyUnit GetNearestEnemy(Vector3 mainUnitPos)
        {
            EnemyUnit nearestEnemy = null;
            float nearestDistanceSqr = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distanceSqr = (enemy.transform.position - mainUnitPos).sqrMagnitude;
                    if (distanceSqr < nearestDistanceSqr)
                    {
                        nearestEnemy = enemy;
                        nearestDistanceSqr = distanceSqr;
                    }
                }
            }

            return nearestEnemy;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 통계 기능
    /// </summary>
    public class StatisticsAbility : MonoBehaviour
    {
        private Unit _unit;

        private Dictionary<int, int> damageStatistic = new Dictionary<int, int>();

        internal void Initialize(Unit unit)
        {
            _unit = unit;

            _unit.healthAbility.onDamage += AddDamage;
        }

        internal void DeInitialize()
        {
            _unit.healthAbility.onDamage -= AddDamage;
        }

        private void AddDamage(int id, int value)
        {
            if (damageStatistic.ContainsKey(id))
            {
                damageStatistic[id] += value;
            }
            else
            {
                damageStatistic[id] = value;
            }
        }

        internal Dictionary<int, int> GetDamage()
        {
            return damageStatistic;
        }
    }
}

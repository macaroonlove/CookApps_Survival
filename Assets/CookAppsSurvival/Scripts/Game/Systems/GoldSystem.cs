using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// 골드를 관리하는 클래스
    /// </summary>
    public class GoldSystem : MonoBehaviour, ISubSystem
    {
        [SerializeField] private ObscuredIntVariable gold;
        private EnemySystem _enemySystem;

        internal UnityAction<int> onChangedGold;

        public void Initialize()
        {
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();
            
            _enemySystem.onDieEnemy += OnDieEnemy;
        }

        public void Deinitialize()
        {
            _enemySystem.onDieEnemy -= OnDieEnemy;
        }

        private void OnDieEnemy(EnemyUnit enemyUnit)
        {
            gold.AddValue(enemyUnit.template.dropGold);
            
            onChangedGold?.Invoke(gold.Value);
        }

        internal int GetGold()
        {
            return gold.Value;
        }

        internal void UseGold(int value)
        {
            gold.AddValue(-value);

            onChangedGold?.Invoke(gold.Value);
        }
    }
}
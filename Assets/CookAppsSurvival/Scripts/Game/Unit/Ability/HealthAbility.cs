using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    public class HealthAbility : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int _maxHp;
        [SerializeField, ReadOnly] private int _currentHp;

        private Unit _unit;

        internal int maxHp => _maxHp;
        internal int minHp { get; set; }
        internal bool IsAlive => _currentHp > 0;


        internal event UnityAction<int> onChangedHealth;
        internal event UnityAction onDeath;

        internal void Initialize(Unit unit)
        {
            _unit = unit;

            float finalMapHp = unit.pureMaxHp;

            // 체력 증감 로직 추후 추가

            _maxHp = (int)finalMapHp;

            SetHp(_maxHp);
        }

        internal void DeInitialize()
        {

        }

        internal bool Damaged(int damage)
        {
            //죽었으면 무시
            if (!IsAlive) return false;

            var lostHealth = damage;

            //잃을 HP 가 있을 때
            if (lostHealth > 0)
            {
                SetHp(_currentHp - lostHealth);

                return true;
            }

            return false;
        }

        internal void Healed(int value)
        {
            //죽었으면 무시
            if (!IsAlive) return;

            //보너스 회복 효과 계산 (추후 구현)
            var bonusHealingEffect = 1.0f;


            var applyBonusHealingEffect = value * bonusHealingEffect;

            var lastHp = Mathf.RoundToInt(_currentHp + applyBonusHealingEffect);
            lastHp = Mathf.Clamp(lastHp, 0, _maxHp);

            SetHp(lastHp);
        }

        private void SetHp(int hp)
        {
            _currentHp = Mathf.Max(minHp, hp);
            if (_currentHp == 0)
            {
                onDeath?.Invoke();
                return;
            }
            onChangedHealth?.Invoke(_currentHp);
        }
    }
}
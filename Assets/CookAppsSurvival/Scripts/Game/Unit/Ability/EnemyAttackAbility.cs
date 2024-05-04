using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class EnemyAttackAbility : AttackAbility
    {
        private EnemyUnit _enemyUnit;

        private PartySystem _partySystem;

        /// <summary>
        /// 최종 공격 간격
        /// </summary>
        protected override float finalAttackTerm
        {
            get
            {
                float final = _pureAttackTerm;

                // 공격 속도 증가 아이템 적용

                //최소공격속도 : 기본 공격속도의 30% 보장
                final = Mathf.Min(final, _pureAttackTerm / 0.3f);

                return final;
            }
        }

        /// <summary>
        /// 최종 공격거리
        /// </summary>
        protected override float finalAttackDistance
        {
            get
            {
                float final = _pureAttackDistance;

                //일부 보정
                final -= 0.1f;

                final = Mathf.Max(final, 0);

                return final;
            }
        }

        internal void Initialize(EnemyUnit enemyUnit)
        {
            this._enemyUnit = enemyUnit;

            _pureATK = enemyUnit.pureATK;
            _pureAttackTerm = enemyUnit.pureAttackTerm;
            _pureAttackDistance = enemyUnit.pureAttackRange;
            _cooldownTime = finalAttackTerm;
            _isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        internal void DeInitialize()
        {
            _isAttackAble = false;
        }

        protected override bool Action()
        {
            if (_enemyUnit.moveAbility.isMove) return false;

            var attackTarget = _enemyUnit.moveAbility.target;

            if (attackTarget != null)
            {
                Attack(attackTarget);
            }

            return true;
        }

        private void Attack(Unit attackTarget)
        {
            // 공격 모션
            _enemyUnit.animationController.Attack();

            // 유닛한테 데미지 주기
            attackTarget.healthAbility.Damaged(_pureATK);
        }
    }
}

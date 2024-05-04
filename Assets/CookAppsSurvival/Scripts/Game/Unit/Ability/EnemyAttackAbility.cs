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

        internal void Initialize(EnemyUnit enemyUnit)
        {
            this._enemyUnit = enemyUnit;

            _pureAttackTerm = enemyUnit.template.attackTerm;
            _pureAttackDistance = enemyUnit.template.attackRange;
            _cooldownTime = finalAttackTerm;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        protected override bool Action()
        {
            if (_enemyUnit.moveAbility.isMove) return false;

            var attackTarget = _partySystem.mainUnit.moveAbility.target;

            Attack(attackTarget);

            return true;
        }

        private void Attack(Unit attackTarget)
        {
            //Debug.Log("실행");
            // 공격 모션
            _enemyUnit.animationController.Attack();
        }
    }
}

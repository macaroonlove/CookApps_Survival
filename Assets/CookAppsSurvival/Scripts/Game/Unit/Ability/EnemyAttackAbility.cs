using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class EnemyAttackAbility : AttackAbility
    {
        private EnemyUnit _enemyUnit;
        private PartySystem _partySystem;

        public override Unit unit => _enemyUnit;

        #region 스탯
        internal override float finalAttackTerm
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

        internal override float finalAttackDistance
        {
            get
            {
                float final = _pureAttackDistance;

                //일부 보정
                final += 0.3f;

                final = Mathf.Max(final, 0);

                return final;
            }
        }

        internal override int finalATK
        {
            get
            {
                int final = _pureATK;

                return final;
            }
        }
        #endregion

        internal void Initialize(EnemyUnit enemyUnit)
        {
            this._enemyUnit = enemyUnit;

            _pureATK = enemyUnit.pureATK;
            _pureAttackTerm = enemyUnit.pureAttackTerm;
            _pureAttackDistance = enemyUnit.pureAttackRange;

            cooldownTime = finalAttackTerm;
            isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();

            base.Initialize();
        }

        internal override void DeInitialize()
        {
            isAttackAble = false;
            base.DeInitialize();
        }

        protected override bool Action()
        {
            if (_enemyUnit.moveAbility.isMove) return false;
            if (_enemyUnit.moveAbility.isPatrol) return false;
            if (_enemyUnit.abnormalStatusAbility.UnableToAttackEffects.Count > 0) return false;

            ExcuteAttack();

            return true;
        }

        #region 기본 공격
        private void ExcuteAttack()
        {
            // 목표로 이동하는 타겟을 공격 타겟으로 설정
            _attackTarget = _enemyUnit.moveAbility.target;

            // 공격 범위 안에 타겟이 들어왔는지
            bool isInRange = IsInRange(_attackTarget);

            if (_attackTarget != null && isInRange)
            {
                // 공격
                AttackAnimation(_attackTarget);
            }
        }
        #endregion
    }
}

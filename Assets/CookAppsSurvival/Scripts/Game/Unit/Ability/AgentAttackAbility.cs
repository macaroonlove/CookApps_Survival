using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AgentAttackAbility : AttackAbility
    {
        private PartyUnit _partyUnit;

        private PartySystem _partySystem;

        /// <summary>
        /// 최종 공격 간격
        /// </summary>
        protected override float finalAttackTerm
        {
            get
            {
                float final = _pureAttackTerm;

                // 공격 속도 증가 아이템 적용 (추후 적용)

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
                final += 0.1f;

                final = Mathf.Max(final, 0);

                return final;
            }
        }

        internal void Initialize(PartyUnit partyUnit)
        {
            this._partyUnit = partyUnit;

            _pureATK = partyUnit.pureATK;
            _pureAttackTerm = partyUnit.pureAttackTerm;
            _pureAttackDistance = partyUnit.pureAttackRange;
            _cooldownTime = finalAttackTerm;
            _isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        internal void DeInitialize()
        {
            _isAttackAble = false;
        }

        private bool IsInRange(Unit attackTarget)
        {
            float sqrDistance = (_partyUnit.transform.position - attackTarget.transform.position).sqrMagnitude;

            return sqrDistance <= finalAttackDistance * finalAttackDistance;

            //float distance = Vector3.Distance(_partyUnit.transform.position, attackTarget.transform.position);

            //return distance <= finalAttackDistance;
        }

        protected override bool Action()
        {
            if (_partyUnit.moveAbility.isMove) return false;

            var attackTarget = _partySystem.mainUnit.moveAbility.target;
            _partyUnit.moveAbility.NewAttackTarget(attackTarget);

            var isInRange = IsInRange(attackTarget);

            if (attackTarget != null && isInRange)
            {
                Attack(attackTarget);
            }

            return true;
        }

        private void Attack(Unit attackTarget)
        {
            // 공격 모션
            _partyUnit.animationController.Attack();

            // 유닛한테 데미지 주기
            attackTarget.healthAbility.Damaged(_pureATK);
        }
    }
}

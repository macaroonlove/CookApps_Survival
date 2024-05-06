using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AgentAttackAbility : AttackAbility
    {
        [SerializeField, ReadOnly] private Unit attackTarget;
        [SerializeField, ReadOnly] private float skillCooldownTime = 0;

        private PartyUnit _partyUnit;

        private PartySystem _partySystem;
        private EnemySystem _enemySystem;

        /// <summary>
        /// 최종 공격 간격
        /// </summary>
        internal override float finalAttackTerm
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
        internal override float finalAttackDistance
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

        /// <summary>
        /// 최종 공격력
        /// </summary>
        internal override int finalATK
        {
            get
            {
                int final = _pureATK;

                // 레벨로 인한 공격력 상승 (임의로 +10씩)
                int level = _partyUnit.GetLevel();

                final += (level - 1) * 10;

                return final;
            }
        }

        internal void Initialize(PartyUnit partyUnit)
        {
            this._partyUnit = partyUnit;

            _pureATK = partyUnit.pureATK;
            _pureAttackTerm = partyUnit.pureAttackTerm;
            _pureAttackDistance = partyUnit.pureAttackRange;
            cooldownTime = finalAttackTerm;
            isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();
        }

        internal void DeInitialize()
        {
            isAttackAble = false;
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
            skillCooldownTime -= Time.deltaTime;
            
            if (_partyUnit.moveAbility.isMove) return false;
            if (_partyUnit.abnormalStatusAbility.UnableToAttackEffects.Count > 0) return false;

            if (skillCooldownTime > 0)
            {
                AttackImpact();
            }
            else
            {
                ExcuteSkill();
            }
            
            return true;
        }

        private void AttackImpact()
        {
            attackTarget = _partySystem.mainUnit.moveAbility.target;
            _partyUnit.moveAbility.NewAttackTarget(attackTarget);

            var isInRange = IsInRange(attackTarget);

            if (attackTarget != null && isInRange)
            {
                Attack(attackTarget);
            }
        }

        private void Attack(Unit attackTarget)
        {
            // 공격 모션
            _partyUnit.animationController.Attack();

            // 유닛한테 데미지 주기
            attackTarget.healthAbility.Damaged(_pureATK, _partyUnit.id);
        }

        private void ExcuteSkill()
        {
            // 스킬 모션
            _partyUnit.animationController.Skill();

            foreach (var effect in _partyUnit.skillTemplate.effects)
            {
                effect.Excute(_partyUnit);
            }
            skillCooldownTime = _partyUnit.skillTemplate.cooldownTime;
        }


        internal List<EnemyUnit> FindAttackTargetEnemies(EEnemyTarget targetCondition, float radius, int maxCount)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();

            switch (targetCondition)
            {
                case EEnemyTarget.ExistingEnemy:
                    enemies.Add(attackTarget as EnemyUnit);
                    break;
                case EEnemyTarget.OneEnemyInRange:
                    enemies.Add(_enemySystem.FindNearestEnemy(_partyUnit.transform.position));
                    break;
                case EEnemyTarget.NumEnemyInRange:
                    enemies.AddRange(_enemySystem.FindEnemiesInRadius(_partyUnit.transform.position, radius, maxCount));
                    break;
                case EEnemyTarget.AllEnemyInRange:
                    enemies.AddRange(_enemySystem.FindEnemiesInRadius(_partyUnit.transform.position, radius));
                    break;
                case EEnemyTarget.AllEnemy:
                    enemies.AddRange(_enemySystem.AllEnemies());
                    break;
            }

            return enemies;
        }

        internal List<PartyUnit> FindHealTargetMembers(EAgentTarget targetCondition, float radius)
        {
            List<PartyUnit> members = new List<PartyUnit>();

            switch (targetCondition)
            {
                case EAgentTarget.Myself:
                    members.Add(_partyUnit);
                    break;
                case EAgentTarget.OneAgentInRange:
                    members.AddRange(_partySystem.FindMembersInRadius(_partyUnit.transform.position, radius, 1));
                    break;
                case EAgentTarget.AllAgentInRange:
                    members.AddRange(_partySystem.FindMembersInRadius(_partyUnit.transform.position, radius));
                    break;
                case EAgentTarget.AllAgentInRangeExceptMe:
                    foreach (var member in _partySystem.FindMembersInRadius(_partyUnit.transform.position, radius))
                    {
                        if (member == _partyUnit) continue;
                        members.Add(member);
                    }
                    break;
            }

            return members;
        }
    }
}

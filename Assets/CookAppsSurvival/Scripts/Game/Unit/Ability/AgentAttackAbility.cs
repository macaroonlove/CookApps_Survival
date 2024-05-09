using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AgentAttackAbility : AttackAbility
    {
        private float skillCooldownTime = 1f;
        private float finalSkillCooldownTime = 1f;

        private PartyUnit _partyUnit;
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;

        private SkillEventHandler _skillEventHandler;
        private bool _isEventSkill;

        internal float skillCooldownAmount => skillCooldownTime / finalSkillCooldownTime;

        public override Unit unit => _partyUnit;

        #region 스탯
        internal override float finalAttackTerm
        {
            get
            {
                float final = _pureAttackTerm;

                float increase = 1;
                // 상점 버프 아이템을 통한 공격속도 증가
                foreach (var effect in _partyUnit.buffAbility.AttackSpeedIncreaseDataEffects)
                {
                    increase += effect.value;
                }

                final /= increase;

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

                // 보스의 경우 사거리 보정
                if (_attackTarget is EnemyUnit enemy)
                {
                    if (enemy.template.enemyType == EEnemyType.Boss)
                    {
                        final += enemy.template.attackRange;
                    }
                }

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
                float final = _pureATK;

                float increase = 1;

                // 레벨로 인한 공격력 추가 (임의로 +10씩)
                int level = _partyUnit.GetLevel();
                final += (level - 1) * 10;

                // 상점 버프 아이템을 통한 공격력 증가
                foreach(var effect in _partyUnit.buffAbility.ATKIncreaseDataEffects)
                {
                    increase += effect.value;
                }

                final *= increase;

                return (int)final;
            }
        }
        #endregion

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

            _skillEventHandler = GetComponentInChildren<SkillEventHandler>();
            if (_skillEventHandler != null)
            {
                _skillEventHandler.onSkill += OnSkillEvent;
                _isEventSkill = true;
            }

            base.Initialize();
        }

        internal override void DeInitialize()
        {
            isAttackAble = false;

            if (_skillEventHandler != null)
            {
                _skillEventHandler.onSkill -= OnSkillEvent;
                _isEventSkill = false;
            }
            base.DeInitialize();
        }

        protected override void Update()
        {
            if (skillCooldownTime > 0)
            {
                skillCooldownTime -= Time.deltaTime;
            }

            base.Update();
        }

        protected override bool Action()
        {            
            if (_partyUnit.moveAbility.isMove) return false;
            if (_partyUnit.abnormalStatusAbility.UnableToAttackEffects.Count > 0) return false;

            if (skillCooldownTime > 0)
            {
                // 기본 공격
                ExcuteAttack();
            }
            else
            {
                // 스킬 공격
                ExcuteSkill();
            }
            
            return true;
        }

        #region 기본 공격
        private void ExcuteAttack()
        {
            // 메인 유닛이 목표로 이동하는 타겟을 공격 타겟으로 설정
            _attackTarget = _partySystem.mainUnit.moveAbility.target;
            
            if (_attackTarget == null) return;

            _partyUnit.moveAbility.NewAttackTarget(_attackTarget);

            // 공격 범위 안에 타겟이 들어왔는지
            bool isInRange = IsInRange(_attackTarget);

            // 공격할 타겟이 있고, 범위 안에 있다면
            if (_attackTarget != null && isInRange)
            {
                // 공격
                AttackAnimation(_attackTarget);
            }
        }
        #endregion

        #region 스킬 공격
        private void ExcuteSkill()
        {            
            // 원하는 목표가 한 유닛이라도 있다면 애니메이션 실행
            foreach (var effect in _partyUnit.skillTemplate.effects)
            {
                // 적을 탐색
                var enemies = effect.GetTarget(_partyUnit);

                // 적이 있다면
                if (enemies.Count > 0 && enemies[0] != null)
                {
                    SkillAnimation();

                    break;
                }
            }
        }

        private void SkillAnimation()
        {
            // 스킬 모션
            _partyUnit.animationController.Skill();

            // 모션이 없는 즉시 발동 스킬의 경우
            if (!_isEventSkill)
            {
                Skill();
            }
        }

        private void OnSkillEvent()
        {
            Skill();
        }

        private void Skill()
        {
            foreach (var effect in _partyUnit.skillTemplate.effects)
            {
                // 적을 탐색
                var enemies = effect.GetTarget(_partyUnit);

                // 스킬 실행
                foreach (var enemy in enemies)
                {
                    effect.Excute(_partyUnit, enemy);
                }
            }

            // 쿨타임 돌리기
            skillCooldownTime = _partyUnit.skillTemplate.cooldownTime;
            finalSkillCooldownTime = skillCooldownTime;
        }
        #endregion


        #region 유틸리티 메서드
        internal List<EnemyUnit> FindAttackTargetEnemies(EEnemyTarget targetCondition, float radius, int maxCount)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();

            switch (targetCondition)
            {
                case EEnemyTarget.OneEnemyInRange:
                    radius = FinalSkillDistance(radius);
                    enemies.Add(_enemySystem.FindNearestEnemyInRange(_partyUnit.transform.position, radius));
                    break;
                case EEnemyTarget.NumEnemyInRange:
                    radius = FinalSkillDistance(radius);
                    enemies.AddRange(_enemySystem.FindEnemiesInRadius(_partyUnit.transform.position, radius, maxCount));
                    break;
                case EEnemyTarget.AllEnemyInRange:
                    radius = FinalSkillDistance(radius);
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

        private float FinalSkillDistance(float radius)
        {
            float final = radius;

            // 보스의 경우 사거리 보정
            if (_attackTarget is EnemyUnit enemy)
            {
                if (enemy.template.enemyType == EEnemyType.Boss)
                {
                    final += enemy.template.attackRange;
                }
            }

            //일부 보정
            final += 1f;

            final = Mathf.Max(final, 0);

            return final;
        }
        #endregion
    }
}
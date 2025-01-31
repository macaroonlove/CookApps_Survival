using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �� ����
    /// </summary>
    [RequireComponent(typeof(EnemyAttackAbility))]
    [RequireComponent(typeof(PatrolAbility))]
    public class EnemyUnit : Unit
    {
        protected EnemyTemplate _template;
        protected EnemyAttackAbility _enemyAttackAbility;
        protected PatrolAbility _patrolAbility;

        public EnemyAttackAbility enemyAttackAbility => _enemyAttackAbility;
        public PatrolAbility patrolAbility => _patrolAbility;

        public int dropExp => _template.dropExp;

        public override int id => _template.id;

        public override int pureATK => _template.ATK;

        public override int pureMaxHp => _template.maxHp;

        public override float pureAttackTerm => _template.attackTerm;

        public override float pureAttackRange => _template.attackRange;

        public override float pureMoveSpeed => _template.moveSpeed;

        public EnemyTemplate template => _template;

        public float trackableDistance => _template.trackableDistance;

        public float patrolRadius => _template.patrolRadius;

        public float patrolWaitTime => _template.patrolWaitTime;

        public void Initialize(EnemyTemplate template)
        {
            _template = template;

            base.Initialize();            

            if (_enemyAttackAbility == null)
            {
                TryGetComponent(out _enemyAttackAbility);
            }

            if (_patrolAbility == null)
            {
                TryGetComponent(out _patrolAbility);
            }

            _enemyAttackAbility.Initialize(this);
            _patrolAbility.Initialize(this);
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            Invoke(nameof(Despawn), 1f);
        }

        private void Despawn()
        {
            BattleManager.Instance.GetSubSystem<SpawnSystem>().DespawnEnemy(this);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();

            _enemyAttackAbility.DeInitialize();
            _patrolAbility.DeInitialize();
        }
    }
}

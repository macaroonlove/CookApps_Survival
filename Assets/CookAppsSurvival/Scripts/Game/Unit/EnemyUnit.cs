using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// Àû À¯´Ö
    /// </summary>
    [RequireComponent(typeof(EnemyAttackAbility))]
    public class EnemyUnit : Unit
    {
        [SerializeField] private EnemyTemplate _template;

        protected EnemyAttackAbility _enemyAttackAbility;

        public EnemyAttackAbility enemyAttackAbility => _enemyAttackAbility;

        public override int pureATK => _template.ATK;

        public override int pureMaxHp => _template.maxHp;

        public override float pureAttackTerm => _template.attackTerm;

        public override float pureAttackRange => _template.attackRange;

        public void Initialize()
        {
            base.Initialize();

            //_template = template;

            if (_enemyAttackAbility == null)
            {
                TryGetComponent(out _enemyAttackAbility);
            }

            _enemyAttackAbility.Initialize(this);
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
        }
    }
}

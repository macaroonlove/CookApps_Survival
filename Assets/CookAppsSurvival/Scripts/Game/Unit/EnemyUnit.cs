using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// Рћ РЏДж
    /// </summary>
    [RequireComponent(typeof(EnemyAttackAbility))]
    public class EnemyUnit : Unit
    {
        [SerializeField] private EnemyTemplate _template;

        protected EnemyAttackAbility _enemyAttackAbility;

        public EnemyAttackAbility enemyAttackAbility => _enemyAttackAbility;

        internal EnemyTemplate template => _template;

        public override void Initialize()
        {
            base.Initialize();

            TryGetComponent(out _enemyAttackAbility);

            _enemyAttackAbility.Initialize(this);
        }
    }
}

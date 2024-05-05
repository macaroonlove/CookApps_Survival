using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ÆÄÆ¼¿ø À¯´Ö
    /// </summary>
    [RequireComponent(typeof(AgentAttackAbility))]
    public class PartyUnit : Unit
    {
        private AgentTemplate _template;

        private PartySystem _partySystem;

        protected AgentAttackAbility _agentAttackAbility;

        public AgentAttackAbility agentAttackAbility => _agentAttackAbility;

        public override int pureATK => _template.ATK;

        public override int pureMaxHp => _template.maxHp;

        public override float pureAttackTerm => _template.attackTerm;

        public override float pureAttackRange => _template.attackRange;

        public EJob job => _template.job;

        public void Initialize(AgentTemplate template = null)
        {
            if (template != null)
            {
                _template = template;
            }

            base.Initialize();

            if (_agentAttackAbility == null)
            {
                TryGetComponent(out _agentAttackAbility);
            }

            _agentAttackAbility.Initialize(this);

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            _partySystem.UnitDieRevival(this);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();

            _agentAttackAbility.DeInitialize();
        }
    }
}

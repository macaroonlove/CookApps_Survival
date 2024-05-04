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
        [SerializeField] private AgentTemplate _template;

        protected AgentAttackAbility _agentAttackAbility;

        public AgentAttackAbility agentAttackAbility => _agentAttackAbility;

        internal AgentTemplate template => _template;

        public override void Initialize()
        {
            base.Initialize();

            TryGetComponent(out _agentAttackAbility);

            _agentAttackAbility.Initialize(this);
        }
    }
}

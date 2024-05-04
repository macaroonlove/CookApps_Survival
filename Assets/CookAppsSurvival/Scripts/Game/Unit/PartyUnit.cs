using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ÆÄÆ¼¿ø À¯´Ö
    /// </summary>
    [RequireComponent(typeof(NavMeshDynamicAgent))]
    public class PartyUnit : Unit
    {
        private NavMeshDynamicAgent _agent;

        public NavMeshDynamicAgent agent => _agent;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _agent);
        }

        void Start()
        {
            _agent.Initialize(this);
        }
    }
}

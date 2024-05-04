using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// Àû À¯´Ö
    /// </summary>
    [RequireComponent(typeof(NavMeshDynamicAgent))]
    public class EnemyUnit : Unit
    {
        private NavMeshDynamicAgent _agent;

        public NavMeshDynamicAgent agent => _agent;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _agent);
        }

        public void Initialize()
        {
            _agent.Initialize(this);
        }
    }
}

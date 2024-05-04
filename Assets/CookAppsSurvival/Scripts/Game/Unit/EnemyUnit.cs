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

        void Start()
        {
            _agent.Initialize(this);
        }

        public void Initialize()
        {
            
        }

        void OnDisable()
        {
            _agent.enabled = false;
        }
    }
}

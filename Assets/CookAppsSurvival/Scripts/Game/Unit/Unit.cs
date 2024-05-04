using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// 모든 유닛의 조상
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Billboarding))]
    [RequireComponent(typeof(UnitAnimationController))]
    public class Unit : MonoBehaviour
    {
        protected UnitAnimationController _animationController;

        public UnitAnimationController animationController => _animationController;

        protected virtual void Awake()
        {
            TryGetComponent(out _animationController);
            
        }

        void Start()
        {
            
        }
    }
}

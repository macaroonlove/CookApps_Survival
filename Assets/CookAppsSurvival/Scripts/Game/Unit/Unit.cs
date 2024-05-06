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
    [RequireComponent(typeof(MoveAbility))]
    [RequireComponent(typeof(HealthAbility))]
    [RequireComponent(typeof(AbnormalStatusAbility))]
    public abstract class Unit : MonoBehaviour
    {
        protected UnitAnimationController _animationController;
        protected MoveAbility _moveAbility;
        protected HealthAbility _healthAbility;
        protected AbnormalStatusAbility _abnormalStatusAbility;

        public UnitAnimationController animationController => _animationController;
        public MoveAbility moveAbility => _moveAbility;
        public HealthAbility healthAbility => _healthAbility;
        public AbnormalStatusAbility abnormalStatusAbility => _abnormalStatusAbility;


        /// <summary>
        /// 순수 공격력
        /// </summary>
        public abstract int pureATK { get; }
        /// <summary>
        /// 순수 최대 체력
        /// </summary>
        public abstract int pureMaxHp { get; }
        
        /// <summary>
        /// 순수 공격 주기
        /// </summary>
        public abstract float pureAttackTerm { get; }
        
        /// <summary>
        /// 순수 공격 범위
        /// </summary>
        public abstract float pureAttackRange { get; }
        
        /// <summary>
        /// 순수 이동 속도
        /// </summary>
        public abstract float pureMoveSpeed { get; }
        

        protected void Initialize()
        {
            if (_animationController == null)
            {
                TryGetComponent(out _animationController);
            }
            
            if (_moveAbility == null)
            {
                TryGetComponent(out _moveAbility);
            }
            
            if (_healthAbility == null)
            {
                TryGetComponent(out _healthAbility);
            }

            if (_abnormalStatusAbility == null)
            {
                TryGetComponent(out _abnormalStatusAbility);
            }

            _animationController.Initialze(this);
            _moveAbility.Initialize(this);
            _healthAbility.Initialize(this);
            _abnormalStatusAbility.Initialize(this);

            _healthAbility.onDeath += OnDeath;
        }

        protected virtual void OnDeath()
        {
            // 죽는 애니메이션
            animationController.Death();

            DeInitialize();
        }

        public virtual void DeInitialize()
        {
            _animationController.DeInitialize();
            _moveAbility.DeInitialize();
            _healthAbility.DeInitialize();
            _abnormalStatusAbility.DeInitialize();

            _healthAbility.onDeath -= OnDeath;
        }
    }
}

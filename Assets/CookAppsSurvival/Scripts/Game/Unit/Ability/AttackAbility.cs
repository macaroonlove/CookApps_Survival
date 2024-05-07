using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        [SerializeField] protected bool isProjectileAttack;
        [SerializeField, Condition("isProjectileAttack", true)] protected GameObject projectilePrefab;
        [SerializeField, Condition("isProjectileAttack", true)] protected Transform projectileSpawnPoint;

        [SerializeField, ReadOnly] protected bool isAttackAble;
        [SerializeField, ReadOnly] protected float cooldownTime = 0;

        protected Unit _attackTarget;
        protected PoolSystem _poolSystem;
        protected int _pureATK;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;

        private AttackEventHandler _attackEventHandler;
        private bool _isEventAttack;

        public abstract Unit unit { get; }

        #region 스탯 추상 메서드
        /// <summary>
        /// 최종 공격 간격
        /// </summary>
        internal abstract float finalAttackTerm { get; }

        /// <summary>
        /// 최종 공격거리
        /// </summary>
        internal abstract float finalAttackDistance { get; }

        /// <summary>
        /// 최종 공격력
        /// </summary>
        internal abstract int finalATK { get; }
        #endregion

        internal void Initialize()
        {
            _poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();

            _attackEventHandler = GetComponentInChildren<AttackEventHandler>();
            if (_attackEventHandler != null)
            {
                _attackEventHandler.onAttack += OnAttackEvent;
                _isEventAttack = true;
            }
        }

        internal virtual void DeInitialize()
        {
            if (_attackEventHandler != null)
            {
                _attackEventHandler.onAttack -= OnAttackEvent;
                _isEventAttack = false;
            }
        }

        protected virtual void Update()
        {
            if (!isAttackAble) return;

            //공격사이 시간 계산
            if (cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
                return;
            }

            //공격 가능상태
            bool isExcute = Action();
            if (isExcute)
            {
                cooldownTime = finalAttackTerm;
            }
        }

        protected abstract bool Action();

        protected virtual void AttackAnimation(Unit attackTarget)
        {
            // 공격 모션
            unit.animationController.Attack();

            if (!_isEventAttack)
            {
                Attack(attackTarget);
            }            
        }

        private void OnAttackEvent()
        {
            Attack(_attackTarget);
        }

        private void Attack(Unit attackTarget)
        {
            // 투사체 공격일 경우
            if (isProjectileAttack)
            {
                // 투사체 생성
                var projectile = _poolSystem.Spawn(projectilePrefab).GetComponent<Projectile>();
                projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, Quaternion.identity);
                projectile.Initialize(this, attackTarget);
            }
            // 즉시 공격일 경우
            else
            {
                AttackImpact(attackTarget);
            }
        }

        internal virtual void AttackImpact(Unit attackTarget)
        {
            // 타겟에게 데미지 주기
            attackTarget.healthAbility.Damaged(finalATK, unit.id);
        }

        protected virtual bool IsInRange(Unit attackTarget)
        {
            float distance = Vector3.Distance(unit.transform.position, attackTarget.transform.position);

            return distance <= finalAttackDistance;
        }

        internal void DeSpawnProjectile(GameObject obj)
        {
            _poolSystem.DeSpawn(obj);
        }
    }
}
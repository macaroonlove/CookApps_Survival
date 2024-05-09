using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 투사체 클래스
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool isLookTarget;
        [SerializeField] private float speed;

        private Unit _caster;
        private Unit _target;
        private AttackAbility _attackAbility;

        private ProjectileDamageSkillEffect _damageSkillEffect;

        // 스킬로 생성된 투사체일 경우
        private bool _isSkillProjectile;

        // 초기화 여부
        private bool isInit;

        internal void Initialize(AttackAbility attackAbility, Unit target)
        {
            _caster = attackAbility.unit;
            _target = target;
            _attackAbility = attackAbility;

            if (target == null || !target.healthAbility.IsAlive)
            {
                DeSpawn();
                return;
            }

            if (isLookTarget)
            {
                transform.GetChild(0).LookAt(_target.projectileHitPoint);
            }

            isInit = true;
        }

        internal void Initialize(ProjectileDamageSkillEffect effect, Unit caster, Unit target)
        {
            _caster = caster;
            _target = target;
            _damageSkillEffect = effect;
            _isSkillProjectile = true;

            if (target == null || !target.healthAbility.IsAlive)
            {
                DeSpawn();
                return;
            }

            if (isLookTarget)
            {
                transform.GetChild(0).LookAt(_target.projectileHitPoint);
            }

            isInit = true;
        }

        private void Update()
        {
            if (isInit == false) return;

            // 날라가는 도중 타겟이 죽었다면
            if (!_target.healthAbility.IsAlive)
            {
                DeSpawn();
                return;
            }

            Move();
        }

        private void Move()
        {
            var projectilePos = this.transform.position;
            var targetPos = _target.projectileHitPoint.position;
            var distance = Vector3.Distance(projectilePos, targetPos);
            var moveDistance = Time.deltaTime * speed;

            // 날라가는 중
            if (distance > moveDistance)
            {
                var dir = (targetPos - projectilePos).normalized;
                var deltaPos = dir * moveDistance;
                this.transform.Translate(deltaPos);
            }
            // 충돌
            else
            {
                OnCollision();
            }
        }

        private void OnCollision()
        {
            if (_isSkillProjectile)
            {
                _damageSkillEffect.SkillImpact(_caster, _target);
            }
            else
            {
                _attackAbility.AttackImpact(_target);
            }
            
            DeSpawn();
        }

        private void DeSpawn()
        {
            BattleManager.Instance.GetSubSystem<PoolSystem>().DeSpawn(gameObject);
            isInit = false;
        }
    }
}
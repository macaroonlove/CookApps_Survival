using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/ProjectileDamage", fileName = "Skill_ProjectileDamage", order = 0)]
    public class ProjectileDamageSkillEffect : SkillEffect
    {
        /// <summary>
        /// 피해 대상
        /// </summary>
        [SerializeField] private EEnemyTarget damageTarget;

        /// <summary>
        /// 범위 지정
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.OneEnemyInRange, (int)EEnemyTarget.NumEnemyInRange, (int)EEnemyTarget.AllEnemyInRange)]
        [SerializeField] private float radius;

        /// <summary>
        /// 공격할 적의 수
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.NumEnemyInRange)]
        [SerializeField] private int numberOfEnemies;

        /// <summary>
        /// 데미지 비례 타입
        /// </summary>
        [SerializeField] private EPercentageType damageType;

        /// <summary>
        /// 피해량
        /// </summary>
        [SerializeField] private float damageAmountPer;

        /// <summary>
        /// 프리팹
        /// </summary>
        [Space(20), SerializeField] private GameObject _prefab;

        /// <summary>
        /// 투사체 스폰 위치에서 생성할 것인지
        /// </summary>
        [SerializeField] private bool isUseProjectileSpwanPoint;

        /// <summary>
        /// 스폰 위치 오프셋
        /// </summary>
        [SerializeField] private Vector3 _offset;

        /// <summary>
        /// fx
        /// </summary>
        [SerializeField] FX muzzleFx;
        [SerializeField] FX hitFx;

        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> enemies = new List<Unit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(damageTarget, radius, numberOfEnemies));

            return enemies;
        }

        public override void Excute(PartyUnit unit, Unit enemy)
        {
            if (_prefab == null) return;
            if (enemy == null || unit == null) return;
            if (!enemy.healthAbility.IsAlive) return;

            Vector3 spawnPoint = unit.transform.position;
            if (isUseProjectileSpwanPoint)
            {
                spawnPoint = unit.attackAbility.projectileSpawnPointVector;
            }

            var poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();

            var projectile = poolSystem.Spawn(_prefab).GetComponent<Projectile>();
            projectile.transform.SetPositionAndRotation(spawnPoint + _offset, Quaternion.identity);
            projectile.Initialize(this, unit, enemy);

            
            if (muzzleFx != null)
            {
                muzzleFx.Play(enemy, unit);
            }
        }

        public void SkillImpact(Unit unit, Unit enemy)
        {
            var damage = GetAmount(unit);

            enemy.healthAbility.Damaged(damage, unit.id);

            if (hitFx != null)
            {
                hitFx.Play(enemy, unit);
            }
        }

        public int GetAmount(Unit partyUnit)
        {
            int amount;
            float typeValue = 0f;
            if (damageType == EPercentageType.ATK) typeValue = partyUnit.pureATK;
            else if (damageType == EPercentageType.MaxHP) typeValue = partyUnit.healthAbility.maxHp;

            amount = (int)(typeValue * damageAmountPer);

            return amount;
        }
    }
}

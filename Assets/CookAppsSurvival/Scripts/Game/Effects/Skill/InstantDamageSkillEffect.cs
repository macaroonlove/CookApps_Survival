using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/InstantDamage", fileName = "Skill_InstantDamage", order = 0)]
    public class InstantDamageSkillEffect : SkillEffect
    {
        /// <summary>
        /// 피해 대상
        /// </summary>
        [SerializeField] private EEnemyTarget damageTarget;

        /// <summary>
        /// 범위 지정
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.NumEnemyInRange, (int)EEnemyTarget.AllEnemyInRange)]
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

        public override bool Excute(PartyUnit unit)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(damageTarget, radius, numberOfEnemies));

            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy != null && !enemy.healthAbility.IsAlive) continue;

                    var damage = GetAmount(unit);

                    enemy.healthAbility.Damaged(damage, unit.id);
                }
            }

            return true;
        }

        public int GetAmount(PartyUnit partyUnit)
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

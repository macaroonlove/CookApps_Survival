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
        public EEnemyTarget damageTarget;

        /// <summary>
        /// 범위 지정
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.NumEnemyInRange, (int)EEnemyTarget.AllEnemyInRange)]
        public float radius;

        /// <summary>
        /// 공격할 적의 수
        /// </summary>
        [EnumCondition("damageTarget", (int)EEnemyTarget.NumEnemyInRange)]
        public int numberOfEnemies;

        /// <summary>
        /// 데미지 비례 타입
        /// </summary>
        public EPercentageType damageType;

        /// <summary>
        /// 피해량
        /// </summary>
        public float damageAmountPer;

        public override bool Excute(PartyUnit unit)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(damageTarget, radius, numberOfEnemies));

            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (!enemy.healthAbility.IsAlive) continue;

                    var damage = GetAmount(unit);

                    enemy.healthAbility.Damaged(damage);
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

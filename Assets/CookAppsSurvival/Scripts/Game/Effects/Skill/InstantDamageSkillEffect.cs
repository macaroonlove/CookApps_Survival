using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        [SerializeField] private float radius;

        /// <summary>
        /// 공격할 적의 수
        /// </summary>
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
        /// fx
        /// </summary>
        [SerializeField] FX fx;

        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> enemies = new List<Unit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(damageTarget, radius, numberOfEnemies));

            return enemies;
        }

        public override void Excute(PartyUnit unit, Unit enemy)
        {
            if (unit == null || enemy == null) return;
            if (!enemy.healthAbility.IsAlive) return;

            var damage = GetAmount(unit);

            enemy.healthAbility.Damaged(damage, unit.id);

            if (fx != null)
            {
                fx.Play(enemy, unit);
            }
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

        public override string GetLabel()
        {
            return "즉시 데미지 스킬";
        }

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {
            var labelRect = new Rect(rect.x, rect.y, 140, rect.height);
            var valueRect = new Rect(rect.x + 140, rect.y, rect.width - 140, rect.height);

            GUI.Label(labelRect, "피해 대상");
            damageTarget = (EEnemyTarget)EditorGUI.EnumPopup(valueRect, damageTarget);

            if (damageTarget != EEnemyTarget.AllEnemy)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "범위");
                radius = EditorGUI.FloatField(valueRect, radius);
            }

            if (damageTarget == EEnemyTarget.NumEnemyInRange)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "공격할 적의 수");
                numberOfEnemies = EditorGUI.IntField(valueRect, numberOfEnemies);
            }

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "데미지 비례 타입");
            damageType = (EPercentageType)EditorGUI.EnumPopup(valueRect, damageType);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "피해량");
            damageAmountPer = EditorGUI.FloatField(valueRect, damageAmountPer);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "FX");
            fx = (FX)EditorGUI.ObjectField(valueRect, fx, typeof(FX), false);
        }

        public override int GetNumRows()
        {
            int rowNum = 4;

            if (damageTarget != EEnemyTarget.AllEnemy)
            {
                rowNum += 1;
            }

            if (damageTarget == EEnemyTarget.NumEnemyInRange)
            {
                rowNum += 1;
            }

            return rowNum;
        }
#endif
    }
}

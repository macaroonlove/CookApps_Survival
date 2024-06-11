using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/InstantAbnormalStatus", fileName = "Skill_InstantAbnormalStatus", order = 0)]
    public class InstantAbnormalStatusSkillEffect : SkillEffect
    {
        /// <summary>
        /// 피해 대상
        /// </summary>
        [SerializeField] private EEnemyTarget abnormalTarget;

        /// <summary>
        /// 범위 지정
        /// </summary>
        [SerializeField] private float radius;

        /// <summary>
        /// 상태이상 걸리게 할 적의 수
        /// </summary>
        [SerializeField] private int numberOfEnemies;

        /// <summary>
        /// 상태이상 종류
        /// </summary>
        [SerializeField] private AbnormalStatusTemplate abnormalStatus;

        /// <summary>
        /// 상태이상 지속시간
        /// </summary>
        [SerializeField] private float duration;
        
        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> enemies = new List<Unit>();
            enemies.AddRange(unit.agentAttackAbility.FindAttackTargetEnemies(abnormalTarget, radius, numberOfEnemies));

            return enemies;
        }

        public override void Excute(PartyUnit unit, Unit enemy)
        {
            if (unit == null || enemy == null) return;
            if (!enemy.healthAbility.IsAlive) return;

            enemy.abnormalStatusAbility.ApplyAbnormalStatus(abnormalStatus, duration);
        }

        public override string GetLabel()
        {
            return "즉시 상태이상 스킬";
        }

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {
            var labelRect = new Rect(rect.x, rect.y, 140, rect.height);
            var valueRect = new Rect(rect.x + 140, rect.y, rect.width - 140, rect.height);

            GUI.Label(labelRect, "피해 대상");
            abnormalTarget = (EEnemyTarget)EditorGUI.EnumPopup(valueRect, abnormalTarget);

            if (abnormalTarget != EEnemyTarget.AllEnemy)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "범위");
                radius = EditorGUI.FloatField(valueRect, radius);
            }

            if (abnormalTarget == EEnemyTarget.NumEnemyInRange)
            {
                labelRect.y += 20;
                valueRect.y += 20;
                GUI.Label(labelRect, "공격할 적의 수");
                numberOfEnemies = EditorGUI.IntField(valueRect, numberOfEnemies);
            }            

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "데미지 비례 타입");
            abnormalStatus = (AbnormalStatusTemplate)EditorGUI.ObjectField(valueRect, abnormalStatus, typeof(AbnormalStatusTemplate), false);

            labelRect.y += 20;
            valueRect.y += 20;
            GUI.Label(labelRect, "상태이상 지속시간");
            duration = EditorGUI.FloatField(valueRect, duration);
        }

        public override int GetNumRows()
        {
            int rowNum = 3;

            if (abnormalTarget != EEnemyTarget.AllEnemy)
            {
                rowNum += 1;
            }

            if (abnormalTarget == EEnemyTarget.NumEnemyInRange)
            {
                rowNum += 1;
            }

            return rowNum;
        }
#endif
    }
}

using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Skill/InstantHeal", fileName = "Skill_InstantHeal", order = 0)]
    public class InstantHealSkillEffect : SkillEffect
    {
        /// <summary>
        /// 피해 대상
        /// </summary>
        [SerializeField] private EAgentTarget healTarget;

        /// <summary>
        /// 범위 지정
        /// </summary>
        [EnumCondition("healTarget", (int)EAgentTarget.OneAgentInRange, (int)EAgentTarget.AllAgentInRange, (int)EAgentTarget.AllAgentInRangeExceptMe)]
        [SerializeField] private float radius;

        /// <summary>
        /// 회복 타입
        /// </summary>
        [SerializeField] private EPercentageType healType;

        /// <summary>
        /// 피해량
        /// </summary>
        [SerializeField] private float damageAmountPer;

        /// <summary>
        /// FX
        /// </summary>
        [SerializeField] private FX fx;

        public override List<Unit> GetTarget(PartyUnit unit)
        {
            List<Unit> agents = new List<Unit>();
            agents.AddRange(unit.agentAttackAbility.FindHealTargetMembers(healTarget, radius));

            return agents;
        }

        public override void Excute(PartyUnit unit, Unit agent)
        {
            if (unit == null || agent == null) return;
            if (!agent.healthAbility.IsAlive) return;

            var healAmount = GetAmount(unit);

            agent.healthAbility.Healed(healAmount);

            if (fx != null)
            {
                fx.Play(agent, unit);
            }
        }

        public int GetAmount(PartyUnit partyUnit)
        {
            int amount;
            float typeValue = 0f;
            if (healType == EPercentageType.ATK) typeValue = partyUnit.agentAttackAbility.finalATK;
            else if (healType == EPercentageType.MaxHP) typeValue = partyUnit.healthAbility.maxHp;

            amount = (int)(typeValue * damageAmountPer);

            return amount;
        }
    }
}
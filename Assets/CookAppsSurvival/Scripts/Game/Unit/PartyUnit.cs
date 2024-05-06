using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ÆÄÆ¼¿ø À¯´Ö
    /// </summary>
    [RequireComponent(typeof(AgentAttackAbility))]
    public class PartyUnit : Unit
    {
        private AgentTemplate _template;

        private PartySystem _partySystem;
        private LevelSystem _levelSystem;

        protected AgentAttackAbility _agentAttackAbility;

        public AgentAttackAbility agentAttackAbility => _agentAttackAbility;

        public override int id => _template.id;

        public override int pureATK => _template.ATK;

        public override int pureMaxHp => _template.maxHp;

        public override float pureAttackTerm => _template.attackTerm;

        public override float pureAttackRange => _template.attackRange;

        public override float pureMoveSpeed => _template.moveSpeed;

        public AgentTemplate template => _template;

        public AgentSkillTemplate skillTemplate => _template.skillTemplate;

        public EJob job => _template.job;

        public void Initialize(AgentTemplate template = null)
        {
            if (template != null)
            {
                _template = template;
            }

            base.Initialize();

            if (_agentAttackAbility == null)
            {
                TryGetComponent(out _agentAttackAbility);
            }

            _agentAttackAbility.Initialize(this);

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _levelSystem = BattleManager.Instance.GetSubSystem<LevelSystem>();
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            _partySystem.UnitDieRevival(this);
        }

        public override void DeInitialize()
        {
            base.DeInitialize();

            _agentAttackAbility.DeInitialize();
        }

        #region ·¹º§¾÷ & °æÇèÄ¡ È¹µæ
        internal int GetLevel()
        {
            return _template.Level;
        }

        internal int GetExp()
        {
            return _template.EXP;
        }

        internal float GetNeedExp()
        {
            return _levelSystem.GetNeedExp(this);
        }

        internal int GainEXP(int exp)
        {
            _template.EXP += exp;

            return _template.EXP;
        }

        internal int LevelUp(int RemainingExp)
        {
            _template.Level++;
            _template.EXP = RemainingExp;

            return template.Level;
        }

        internal void ClearExpLevel()
        {
            _template.EXP = 0;
            _template.Level = 1;
        }
        #endregion
    }
}

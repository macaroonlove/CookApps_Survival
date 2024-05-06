using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class UI_ShopSkillUpgradeItem : UI_ShopItem
    {
        [SerializeField] SkillItemTemplate skillItem;
        [SerializeField] GameObject soldOut;

        private PartySystem _partySystem;

        public void Initialize()
        {
            base.Initialize(skillItem.face, skillItem.desc, skillItem.price);

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();

            var members = _partySystem.GetAllMembers();
            if (members[(int)skillItem.job].template.isUpgade)
            {
                soldOut.SetActive(true);
            }
        }

        protected override void BuyItem()
        {
            if (price > _goldSystem.GetGold()) return;

            var members = _partySystem.GetAllMembers();
            members[(int)skillItem.job].template.isUpgade = true;

            soldOut.SetActive(true);
            _goldSystem.UseGold(price);
        }
    }
}
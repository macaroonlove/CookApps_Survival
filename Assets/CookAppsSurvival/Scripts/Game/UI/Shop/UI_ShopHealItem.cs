using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class UI_ShopHealItem : UI_ShopItem
    {
        [SerializeField] HealItemTemplate healItem;

        private PartySystem _partySystem;

        public void Initialize()
        {
            base.Initialize(healItem.face, healItem.desc, healItem.price);

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        protected override void BuyItem()
        {
            if (price > _goldSystem.GetGold()) return;

            var members = _partySystem.GetAllMembers();
            foreach (var member in members)
            {
                var maxHp = member.healthAbility.maxHp;
                member.healthAbility.Healed((int)(maxHp * healItem.healPer));
            }

            _goldSystem.UseGold(price);
        }
    }
}

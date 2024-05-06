using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class UI_ShopBuffItem : UI_ShopItem
    {
        [SerializeField] BuffItemTemplate buffItem;

        private PartySystem _partySystem;

        public void Initialize()
        {
            base.Initialize(buffItem.face, buffItem.desc, buffItem.price);

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        protected override void BuyItem()
        {
            if (price > _goldSystem.GetGold()) return;

            var members = _partySystem.GetAllMembers();
            foreach (var member in members)
            {
                member.buffAbility.ApplyBuff(buffItem.template);
            }

            _goldSystem.UseGold(price);
        }
    }
}

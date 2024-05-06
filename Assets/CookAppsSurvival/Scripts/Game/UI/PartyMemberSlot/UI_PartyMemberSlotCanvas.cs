using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class UI_PartyMemberSlotCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject item;
        public void Initialize(List<PartyUnit> partyUnits)
        {
            foreach(var unit in partyUnits)
            {
                var item = Instantiate(this.item, transform).GetComponent<UI_PartyMemberSlotItem>();
                item.Initialize(unit);
            }
        }
    }
}
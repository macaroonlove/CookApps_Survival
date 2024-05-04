using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// 아군 유닛 파티를 관리하는 시스템
    /// </summary>
    public class PartySystem : MonoBehaviour, ISubSystem
    {
        private List<PartyUnit> _partyUnits = new List<PartyUnit>();

        internal UnityAction<PartyUnit> onUnitRevival;
        internal UnityAction<PartyUnit> onUnitDie;
    
        internal PartyUnit mainUnit
        {
            get
            {
                foreach(var p in _partyUnits)
                {
                    if (p.gameObject.activeSelf)
                    {
                        return p;
                    }
                }

                return null;
            }
        }

        public void Initialize()
        {
            _partyUnits = transform.GetChild(0).GetComponentsInChildren<PartyUnit>().ToList();

            foreach (var unit in _partyUnits)
            {
                unit.Initialize();
            }
        }

        public void Deinitialize()
        {
            
        }
    }
}

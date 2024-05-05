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
        [SerializeField] private PartySettingTemplate template;

        private List<PartyUnit> _partyUnits = new List<PartyUnit>();

        private WaitForSeconds deathAnimWaitForSeconds;
        private WaitForSeconds respawnWaitForSeconds;

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
            for(int i = 0; i < template.partyMembers.Count; i++)
            {
                var member = template.partyMembers[i];

                var unit = Instantiate(member.prefab, transform.GetChild(0).position + Vector3.right * i, Quaternion.identity, transform.GetChild(0));
                var partyUnit = unit.GetComponent<PartyUnit>();
                partyUnit.Initialize(member);
                _partyUnits.Add(partyUnit);
            }

            deathAnimWaitForSeconds = new WaitForSeconds(template.deathAnimTime);
            respawnWaitForSeconds = new WaitForSeconds(template.respawnTime - template.deathAnimTime);

            StartCoroutine(CoInitialize());
        }

        private IEnumerator CoInitialize()
        {
            yield return null;
            onUnitRevival?.Invoke(mainUnit);
        }

        public void Deinitialize()
        {
            foreach(var partyUnit in _partyUnits)
            {
                Destroy(partyUnit);
            }
            _partyUnits.Clear();
        }


        internal void UnitDieRevival(PartyUnit unit)
        {
            StartCoroutine(CoUnitDieRevival(unit));
        }

        private IEnumerator CoUnitDieRevival(PartyUnit unit)
        {
            yield return deathAnimWaitForSeconds;
            unit.gameObject.SetActive(false);
            onUnitDie?.Invoke(mainUnit);

            yield return respawnWaitForSeconds;
            unit.transform.position = GetCenterPosition(unit);
            unit.gameObject.SetActive(true);
            unit.Initialize();
            onUnitRevival?.Invoke(mainUnit);
        }

        private Vector3 GetCenterPosition(PartyUnit unit)
        {
            Vector3 sumPosition = Vector3.zero;

            foreach (PartyUnit partyUnit in _partyUnits)
            {
                if (partyUnit != unit)
                {
                    sumPosition += partyUnit.transform.position;
                }
            }

            Vector3 centerPosition = sumPosition / (_partyUnits.Count - 1);

            return centerPosition;
        }
    }
}
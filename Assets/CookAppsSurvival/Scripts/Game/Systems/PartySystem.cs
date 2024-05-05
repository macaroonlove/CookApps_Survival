using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private UI_PartyMemberSlotCanvas ui_PartyMemberSlot;

        private List<PartyUnit> _partyUnits = new List<PartyUnit>();

        private WaitForSeconds deathAnimWaitForSeconds;
        private WaitForSeconds respawnWaitForSeconds;

        internal UnityAction<PartyUnit> onUnitRevival;
        internal UnityAction<PartyUnit> onUnitDie;
        internal UnityAction onGameEnd;
    
        internal PartyUnit mainUnit
        {
            get
            {
                if (_partyUnits.Count > 0)
                {
                    return _partyUnits[0];
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

            ui_PartyMemberSlot.Initialize(template.partyMembers);

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
            _partyUnits.RemoveAt(0);
            if (_partyUnits.Count <= 0)
            {
                // 게임 종료
                onGameEnd?.Invoke();
            }

            StartCoroutine(CoUnitDieRevival(unit));
        }

        private IEnumerator CoUnitDieRevival(PartyUnit unit)
        {
            onUnitDie?.Invoke(mainUnit);

            yield return deathAnimWaitForSeconds;
            unit.gameObject.SetActive(false);

            yield return respawnWaitForSeconds;
            unit.transform.position = GetCenterPosition();
            unit.gameObject.SetActive(true);
            unit.Initialize();
            _partyUnits.Insert(0, unit);
            onUnitRevival?.Invoke(mainUnit);
        }

        private Vector3 GetCenterPosition()
        {
            Vector3 sumPosition = Vector3.zero;

            foreach (PartyUnit partyUnit in _partyUnits)
            {
                sumPosition += partyUnit.transform.position;
            }

            Vector3 centerPosition = sumPosition / _partyUnits.Count;

            return centerPosition;
        }

        /// <summary>
        /// 범위 내 파티 멤버 찾기
        /// </summary>
        internal List<PartyUnit> FindMembersInRadius(Vector3 unitPos, float radius, int maxCount = int.MaxValue)
        {
            List<PartyUnit> members = new List<PartyUnit>();

            foreach (PartyUnit member in _partyUnits)
            {
                if (members.Count >= maxCount) break;

                if (member != null && member.isActiveAndEnabled)
                {
                    var diff = member.transform.position - unitPos;

                    var distance = diff.magnitude;

                    if (distance <= radius)
                    {
                        if (members.Contains(member) == false)
                        {
                            members.Add(member);
                        }
                    }
                }
            }

            return members;
        }
    }
}
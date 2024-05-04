using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CookApps.Game
{
    public class GetCameraTarget : MonoBehaviour
    {
        private CinemachineVirtualCamera _vCam;
        private PartySystem _partySystem;

        void Awake()
        {
            TryGetComponent(out _vCam);
        }

        void Start()
        {
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();

            _partySystem.onUnitRevival += NewTarget;
            _partySystem.onUnitDie += NewTarget;

            NewTarget(_partySystem.mainUnit);
        }

        void NewTarget(PartyUnit mainUnit)
        {
            var pos = mainUnit.transform;
            _vCam.Follow = pos;
            _vCam.LookAt = pos;
        }

        void OnDestroy()
        {
            _partySystem.onUnitRevival -= NewTarget;
            _partySystem.onUnitDie -= NewTarget;
        }
    }
}

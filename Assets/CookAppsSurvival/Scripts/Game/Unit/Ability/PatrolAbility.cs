using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class PatrolAbility : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Vector3 _targetPosition;

        private EnemyUnit _unit;
        private PartySystem _partySystem;

        private float _patrolRadius;
        private float _patrolWaitTime;
        private bool _isWaiting;
        private float _waitTimer;

        internal void Initialize(EnemyUnit unit)
        {
            _unit = unit;
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();

            _patrolRadius = unit.patrolRadius;
            _patrolWaitTime = unit.patrolWaitTime;

            SetRandomTargetPosition();
        }

        internal void DeInitialize()
        {

        }

        void Update()
        {
            if (!_unit.healthAbility.IsAlive) return;

            var dist = Vector3.Distance(_partySystem.mainUnit.transform.position, transform.position);
            
            if (dist < _unit.trackableDistance)
            {
                _unit.moveAbility.SetIsPatrol(false);
            }
            else
            {
                _unit.moveAbility.SetIsPatrol(true);

                // 패트롤 기능 추가
                if (!_isWaiting)
                {
                    MoveToTargetPosition();
                }
                else
                {
                    UpdateWaitTimer();
                }
            }
        }

        void MoveToTargetPosition()
        {
            _unit.moveAbility.NewDestination(_targetPosition);

            if (Mathf.Abs(transform.position.x - _targetPosition.x) < 0.1f && Mathf.Abs(transform.position.z - _targetPosition.z) < 0.1f)
            {
                // 도착하면 대기 상태로 전환
                _isWaiting = true;
                _waitTimer = _patrolWaitTime;
            }
        }

        void UpdateWaitTimer()
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
            {
                _isWaiting = false;
                SetRandomTargetPosition();
            }
        }

        void SetRandomTargetPosition()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            Vector3 randomOffset = new Vector3(randomDirection.x, 0f, randomDirection.y) * _patrolRadius;

            _targetPosition = _unit.transform.position + randomOffset;
        }
    }
}

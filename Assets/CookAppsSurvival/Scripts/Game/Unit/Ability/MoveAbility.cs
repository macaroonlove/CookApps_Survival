using DG.Tweening;
using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// 파티원 및 적 유닛이 파티의 리더를 쫓아가도록 하는 클래스
    /// </summary>
    public class MoveAbility : MonoBehaviour
    {
        [SerializeField] private Transform _targetPos;
        [SerializeField] private bool _isReverse;

        [SerializeField, ReadOnly] private bool _isMove;
        [SerializeField, ReadOnly] private bool _isMainUnit;
        [SerializeField, ReadOnly] private Unit _target;
        [SerializeField, ReadOnly] private bool _isPatrol;

        private Unit _unit;
        private NavMeshAgent _agent;
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private SpawnSystem _spawnSystem;
        private BossSystem _bossSystem;
        

        public bool isMove => _isMove;
        public bool isPatrol => _isPatrol;

        public Unit target => _target;
        public Transform targetPos => _targetPos;

        /// <summary>
        /// 최종 이동속도
        /// </summary>
        internal float finalMoveSpeed
        {
            get
            {
                float final = _unit.pureMoveSpeed;

                float increase = 1;
                // 상점 버프 아이템을 통한 공격속도 증가
                foreach (var effect in _unit.buffAbility.MoveSpeedIncreaseDataEffects)
                {
                    increase += effect.value;
                }

                final *= increase;

                return final;
            }
        }

        internal void Initialize(Unit unit)
        {
            _unit = unit;

            if (TryGetComponent(out _agent))
            {
                _agent.enabled = true;
                _agent.stoppingDistance = unit.pureAttackRange;
            }
            else
            {
                Debug.LogError("네브메시 에이전트를 찾지 못했습니다.\n" +
                    "해당 오브젝트를 비활성화 합니다.");
                gameObject.SetActive(false);
            }

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();
            _spawnSystem = BattleManager.Instance.GetSubSystem<SpawnSystem>();
            _bossSystem = BattleManager.Instance.GetSubSystem<BossSystem>();

            _partySystem.onUnitRevival += NewTarget;
            _partySystem.onUnitDie += NewTarget;
            _spawnSystem.onCompleteSpawn += NewTarget;
            _bossSystem.onVictory += Victory;
        }

        internal void DeInitialize()
        {
            _partySystem.onUnitRevival -= NewTarget;
            _partySystem.onUnitDie -= NewTarget;
            _spawnSystem.onCompleteSpawn -= NewTarget;
            _bossSystem.onVictory -= Victory;
        }

        void Victory()
        {
            this.enabled = false;

            _unit = null;
        }

        void NewTarget(PartyUnit mainUnit)
        {
            if (_unit != mainUnit)
            {
                _targetPos = mainUnit.transform;
                _target = mainUnit;
                _isMainUnit = false;
            }
            else
            {
                _targetPos = null;
                _isMainUnit = true;
            }
        }

        internal void NewAttackTarget(Unit target)
        {
            if (target != null)
            {
                _targetPos = target.transform;
            }
        }

        internal void FocusBossTarget(Unit boss)
        {
            _targetPos = boss.transform;
            _target = boss;
        }

        internal void NewDestination(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        internal void SetIsPatrol(bool isPatrol)
        {
            _isPatrol = isPatrol;
        }

        void Update()
        {
            if (_isPatrol) return;
            if (!_unit.healthAbility.IsAlive) return;
            if (_unit.abnormalStatusAbility.UnableToMoveEffects.Count > 0) return;
            if (_unit == null) return;

            // 타겟이 없을 경우
            if (_targetPos == null || !_targetPos.gameObject.activeSelf)
            {
                if (_isMainUnit)
                {
                    // 자신을 기준으로 가장 가까운 적을 찾기
                    _target = _enemySystem.FindNearestEnemy(_unit.transform.position);
                    if (_target != null)
                    {
                        _targetPos = _target.transform;
                    }
                }
                else
                {
                    _targetPos = _target.transform;
                }
            }

            // 이동
            if (_agent != null && _targetPos != null)
            {
                _agent.speed = finalMoveSpeed;
                _agent.SetDestination(_targetPos.position);

                if (_agent.isActiveAndEnabled && !_agent.isStopped && _agent.velocity.magnitude > 0.3f)
                {
                    _isMove = true;
                }
                else
                {
                    _isMove = false;
                }
            }

            // 회전
            FlipUnit();
        }

        internal bool IsUnitLeft()
        {
            if (_targetPos == null || _unit == null) return false;
            Vector3 directionToUnit = _unit.transform.position - _targetPos.position;
            Vector3 unitRight = _unit.transform.forward;
            float angle = Vector3.SignedAngle(directionToUnit, unitRight, Vector3.up);

            return angle > 0f;
        }

        void FlipUnit()
        {
            bool isLeft = IsUnitLeft();

            float scaleX = isLeft ^ _isReverse ? 1f : -1f;
            transform.GetChild(1).DOScaleX(scaleX, 0.1f);
        }
    }
}

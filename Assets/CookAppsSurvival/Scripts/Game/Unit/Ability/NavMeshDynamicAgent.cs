using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CookApps.Game
{
    /// <summary>
    /// 파티원 및 적 유닛이 파티의 리더(탱커)를 쫓아가도록 하는 클래스
    /// </summary>
    public class NavMeshDynamicAgent : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Unit _unit;
        private NavMeshAgent _agent;

        public void Initialize(Unit unit)
        {
            _unit = unit;

            if (TryGetComponent(out _agent))
            {
                _agent.enabled = true;
            }
            else
            {
                Debug.LogError("네브메시 에이전트를 찾지 못했습니다.\n" +
                    "해당 오브젝트를 비활성화 합니다.");
                gameObject.SetActive(false);
            }
            

            if (_target == null)
            {
                // AgentSystem에 메인 유닛 들고오기
            }
        }

        void Update()
        {
            if (_agent != null && _target != null)
            {
                _agent.SetDestination(_target.position);

                if (_agent.isActiveAndEnabled && !_agent.isStopped && _agent.velocity.magnitude > 0.2f)
                {
                    _unit.animationController.Move(true);
                }
                else
                {
                    _unit.animationController.Move(false);
                }
            }
        }
    }
}

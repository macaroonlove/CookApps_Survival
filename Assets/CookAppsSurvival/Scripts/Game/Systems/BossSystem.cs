using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// 보스를 관리하는 시스템
    /// </summary>
    public class BossSystem : MonoBehaviour, ISubSystem
    {
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private SpawnSystem _spawnSystem;

        private EnemyTemplate _template;
        private EnemyUnit _instance;
        private int _bossCnt;
        private int _currentKillCnt;

        internal UnityAction onVictory;

        public void Initialize(StageTemplate stage)
        {
            _template = stage.bossEnemy;
            _bossCnt = stage.killUntilBoss;
            _currentKillCnt = 0;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();
            _spawnSystem = BattleManager.Instance.GetSubSystem<SpawnSystem>();

            _enemySystem.onDieEnemy += BossAppearanceRitual;
        }

        public void Deinitialize()
        {
            _enemySystem.onDieEnemy -= BossAppearanceRitual;
        }

        private void BossAppearanceRitual(EnemyUnit arg0)
        {
            // 처치한 적의 수 증가
            _currentKillCnt++;

            // 목표 처치 수 까지 일반 몬스터를 잡았다면
            if (_bossCnt <= _currentKillCnt)
            {
                _enemySystem.onDieEnemy -= BossAppearanceRitual;

                // 보스 스폰
                _instance = _spawnSystem.SpawnEnemy(_template, _partySystem.mainUnit.transform.position);
                _partySystem.mainUnit.moveAbility.FocusBossTarget(_instance);

                // 모든 일반 몬스터 비활성화
                _spawnSystem.DisableAllEnemy();

                _instance.healthAbility.onDeath += Victory;
            }
        }

        /// <summary>
        /// 게임 승리
        /// </summary>
        private void Victory()
        {
            var members = _partySystem.GetAllMembers();

            foreach(var member in members)
            {
                member.animationController.Victory();
                member.DeInitialize();
            }

            onVictory?.Invoke();

            _instance.healthAbility.onDeath -= Victory;
        }
    }
}
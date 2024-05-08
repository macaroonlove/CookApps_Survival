using FrameWork;
using FrameWork.Sound;
using FrameWork.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] StageLibraryTemplate template;

        [Header("UI")]
        [SerializeField] CanvasGroupController ui_Opening;
        [SerializeField] CanvasGroupController ui_GameClear;
        [SerializeField] CanvasGroupController ui_Victory;
        [SerializeField] CanvasGroupController ui_Default;

        [Header("사운드")]
        [SerializeField] AudioClip clip_GameStart;

        private BossSystem _bossSystem;
        private PartySystem _partySystem;

        private string activeScene;

        protected override void Start()
        {
            base.Start();

            activeScene = SceneManager.GetActiveScene().name;

            if (activeScene == template.stage[0].sceneName)
            {
                ui_Opening.Show();
                SoundManager.PlayBGM(clip_GameStart);
                return;
            }

            InitializeStage();
        }

        internal void InitializeStage()
        {
            foreach (var stage in template.stage)
            {
                if (stage.sceneName == activeScene)
                {
                    SoundManager.PlayBGM(stage.BGM);
                    BattleManager.Instance.InitializeStage(stage);
                }
            }

            _bossSystem = BattleManager.Instance.GetSubSystem<BossSystem>();
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();


            _bossSystem.onVictory += OnVictory;
            _partySystem.onDefault += OnDefeat;
        }

        private void OnVictory()
        {
            for (int i = 0; i < template.stage.Count; i++)
            {
                var stage = template.stage[i];
                if (stage.sceneName == activeScene)
                {
                    if (i + 1 == template.stage.Count)
                    {
                        // 게임 클리어 UI 보여주기
                        ui_GameClear.Show();
                    }
                    else
                    {
                        // 승리 UI 보여주기
                        ui_Victory.Show();
                        
                    }
                }
            }

            UnityEventClear();
        }

        private void OnDefeat()
        {
            ui_Default.Show();

            UnityEventClear();
        }

        private void UnityEventClear()
        {
            _bossSystem.onVictory -= OnVictory;
            _partySystem.onDefault -= OnDefeat;
        }

        public void Victory()
        {
            for (int i = 0; i < template.stage.Count; i++)
            {
                var stage = template.stage[i];
                if (stage.sceneName == activeScene)
                {
                    // 다음 씬으로 이동
                    SceneManager.LoadScene($"{template.stage[i + 1].sceneName}");
                }
            }    
        }

        public void DefeatOrGameClear()
        {
            // 첫 씬으로 이동
            SceneManager.LoadScene($"{template.stage[0].sceneName}");
        }

        
    }
}

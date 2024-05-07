using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CookApps.Game
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] StageLibraryTemplate template;

        void Start()
        {
            string activeScene = SceneManager.GetActiveScene().name;

            foreach(var stage in template.stage)
            {
                if (stage.sceneName == activeScene)
                {
                    BattleManager.Instance.InitializeStage(stage);
                }
            }
        }
    }
}

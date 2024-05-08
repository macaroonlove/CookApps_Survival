using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CookApps.Game
{
    public class OpeningSceneManager : MonoBehaviour
    {
        [SerializeField] Button button_InGame;

        private void Awake()
        {
            button_InGame.onClick.AddListener(OnClick_InGame);
        }

        private void OnClick_InGame()
        {
            StageManager.Instance.InitializeStage();
        }
    }
}
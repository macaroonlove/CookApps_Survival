using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CookApps.Opening
{
    public class OpeningSceneManager : MonoBehaviour
    {
        [SerializeField] Button button_InGame;
        [SerializeField] Button button_Setting;

        private void Awake()
        {
            button_InGame.onClick.AddListener(OnClick_InGame);
            button_Setting.onClick.AddListener(OnClick_Setting);
        }

        private void OnClick_InGame()
        {
            //SceneLoadingManager.LoadScene("Lobby", true);
            SceneManager.LoadScene("Game");
        }

        private void OnClick_Setting()
        {

        }
    }
}
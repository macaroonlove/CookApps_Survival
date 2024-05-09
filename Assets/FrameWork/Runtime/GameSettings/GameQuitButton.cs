using UnityEngine;
using UnityEngine.UI;

namespace FrameWork.GameSettings
{
    [AddComponentMenu("GameSettings/UI/Game Quit Button")]
    public class GameQuitButton : MonoBehaviour
    {
        [SerializeField] private Button targetElement;

        // ------------------------------------------------------------------------------------------------------------

        private void Reset()
        {
            targetElement = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            if (targetElement == null)
            {
                targetElement = GetComponentInChildren<Button>();
                if (targetElement == null)
                {
                    Debug.Log("[GameQuitButton] Could not find any button component on the GameObject.", gameObject);
                    return;
                }
            }

            targetElement.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // ------------------------------------------------------------------------------------------------------------
    }
}

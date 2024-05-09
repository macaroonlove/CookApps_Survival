using UnityEngine;
using UnityEngine.UI;

namespace FrameWork.GameSettings
{
    [AddComponentMenu("GameSettings/UI/Game Setting Open")]
    public class GameSettingOpenButton : MonoBehaviour
    {
        [SerializeField] private Button targetElement;
        [SerializeField] private GameSettingCanvas canvas;

        // ------------------------------------------------------------------------------------------------------------

        private void Reset()
        {
            targetElement = GetComponentInChildren<Button>();
            canvas = FindAnyObjectByType<GameSettingCanvas>();
        }

        private void Start()
        {
            if (targetElement == null)
            {
                targetElement = GetComponentInChildren<Button>();
                canvas = FindAnyObjectByType<GameSettingCanvas>();
                if (targetElement == null)
                {
                    Debug.Log("[GameSettingOpenButton] Could not find any button component on the GameObject.", gameObject);
                    return;
                }
                if (canvas == null)
                {
                    Debug.Log("[GameSettingCanvas] Could not find component on the GameObject.", gameObject);
                    return;
                }
            }

            targetElement.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            canvas.Show();
        }

        // ------------------------------------------------------------------------------------------------------------
    }
}
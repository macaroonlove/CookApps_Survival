using FrameWork.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.GameSettings
{
    [RequireComponent(typeof(CanvasGroupController))]
    public class GameSettingCanvas : MonoBehaviour
    {
        private CanvasGroupController canvasGroupController;

        private void Awake()
        {
            TryGetComponent(out canvasGroupController);
        }

        internal void Show()
        {
            canvasGroupController.Show();
        }
    }
}

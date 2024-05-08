using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class BlinkImage : MonoBehaviour
    {
        private Image image;
        void Awake()
        {
            if (TryGetComponent(out image))
            {
                image.DOFade(0f, 0.5f).OnComplete(() => {
                    image.DOFade(1f, 0.5f);
                }).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }
}
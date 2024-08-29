using System;
using FriedSynapse.FlowEnt;
using UnityEngine;
using UnityEngine.UI;

namespace MainUI.Leaderboard.Graphics
{
    public class PopupRolloutAnimation : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform rectTransform;
        [SerializeField] 
        private Image backgroundImage;
        [SerializeField] 
        private float timeSeconds = 0.3f;
        [SerializeField]
        private bool onAwake = true;

        private Tween posTween;
        private Tween colourTween;

        private void Awake()
        {
            if (!onAwake)
            {
                return;
            }
            AnimateIn();
        }

        private void OnDestroy()
        {
            StopAll();
        }

        public void AnimateIn(Action onCompleted = null)
        {
            Vector3 startPos = rectTransform.localPosition;
            rectTransform.localPosition = new Vector3(startPos.x, -Screen.height * 2f);
            posTween = new Tween(timeSeconds)
                .For(rectTransform)
                .MoveLocalYTo(startPos.y)
                .SetEasing(Easing.EaseInQuad)
                .Start();

            Color colour = backgroundImage.color;
            Color transparentColour = colour;
            transparentColour.a = 0;
            backgroundImage.color = transparentColour;
            colourTween = new Tween(timeSeconds)
                .For(backgroundImage)
                .AlphaTo(colour.a)
                .OnCompleted(onCompleted)
                .Start();
        }
        
        public void AnimateOut(Action onCompleted = null)
        {
            Vector3 startPos = rectTransform.localPosition;
            posTween = new Tween(timeSeconds)
                .For(rectTransform)
                .MoveLocalTo(new Vector3(startPos.x, -Screen.height * 2f))
                .SetEasing(Easing.EaseInQuad)
                .Start();
            colourTween = new Tween(timeSeconds)
                .For(backgroundImage)
                .AlphaTo(0)
                .OnCompleted(onCompleted)
                .Start();
        }

        private void StopAll()
        {
            posTween?.Stop();
            colourTween?.Stop();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace MainUI.Leaderboard.Graphics
{
    public class AvatarSkeletonAnimationUI : MonoBehaviour
    {
        [SerializeField] 
        private string loadingTextPrefix;
        [SerializeField] 
        private List<string> loadingTextAnimation;
        [SerializeField] 
        private float textAnimationGapSeconds;
        [SerializeField] 
        private Color frameColourFrom;
        [SerializeField] 
        private Color frameColourTo;
        [SerializeField] 
        private float frameColourDeltaSpeed;
        [SerializeField] 
        private GameObject avatarGameObject;
        [SerializeField] 
        private Image frameImage;
        [SerializeField] 
        private TextMeshProUGUI skeletonLabel;

        private bool isAnimating;

        public bool IsAnimating => isAnimating;


        private void Awake()
        {
            StartAnimation();
        }

        public async Task StartAnimation()
        {
            if (IsAnimating)
            {
                Debug.LogWarning("Tried to start skeleton animation though it's already active");
                return;
            }
            isAnimating = true;
            avatarGameObject.SetActive(false);
            skeletonLabel.gameObject.SetActive(true);
            LabelAnimation();
            FrameAnimation();
            while (IsAnimating)
            {
                await Task.Yield();
            }
            if (gameObject)
            {
                avatarGameObject.SetActive(true);
                skeletonLabel.gameObject.SetActive(false);
            }
        }

        private async void LabelAnimation()
        {
            while (IsAnimating)
            {
                skeletonLabel.text = loadingTextPrefix;
                foreach (string add in loadingTextAnimation)
                {
                    await SmartTask.WaitForSeconds(textAnimationGapSeconds);
                    if (!IsAnimating) break;
                    skeletonLabel.text += add;
                }
                await SmartTask.WaitForSeconds(textAnimationGapSeconds);
            }
        }

        private async void FrameAnimation()
        {
            while (IsAnimating)
            {
                frameImage.color = Color.Lerp(frameColourFrom, frameColourTo, Mathf.Abs(Mathf.Sin(Time.time * frameColourDeltaSpeed)));
                await Task.Yield();
            }
            
        }

        public void StopAnimation()
        {
            isAnimating = false;
        }

        private void OnDestroy()
        {
            StopAnimation();
        }
    }
}
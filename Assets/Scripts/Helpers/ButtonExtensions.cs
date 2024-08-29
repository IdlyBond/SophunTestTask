using System;
using UnityEngine.UI;

namespace Helpers
{
    public static class ButtonExtensions
    {
        public static void SetOnClick(this Button button, Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}
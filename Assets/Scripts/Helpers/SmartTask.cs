using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class SmartTask
    {
        public static async Task Yield(int frames = 1)
        {
            for (int i = 0; i < frames; i++)
            {
                await Task.Yield();
            }
        }

        public static async Task WaitUntil(Func<bool> condition, Action onUpdate = null, Func<bool> cancellationCondition = null)
        {
            while (!condition.Invoke())
            {
                onUpdate?.Invoke();
                if (cancellationCondition != null && cancellationCondition.Invoke())
                {
                    return;
                }
                await Task.Yield();
            }
        }

        public static async Task WaitForSeconds(float seconds, Action<float> onUpdate = null, Func<bool> cancellationCondition = null)
        {
            float startTime = Time.time;
            float endTime = Time.time + seconds;
            while (Time.time < endTime)
            {
                onUpdate?.Invoke(1 - (endTime - Time.time) / (endTime - startTime));
                if (cancellationCondition != null && cancellationCondition.Invoke())
                {
                    return;
                }
                await Task.Yield();
            }
        }
        
    }
}
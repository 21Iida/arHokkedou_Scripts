using System;
using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// ARモード時に、スマホのスリープ機能をオフにします
    /// </summary>
    public class SleepLock : MonoBehaviour
    {
        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnApplicationQuit()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }

        public void SleepDefault()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
    }
}

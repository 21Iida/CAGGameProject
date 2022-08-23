using System;
using UnityEngine;

namespace General
{
    public class TimeReset : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1f;
        }
    }
}

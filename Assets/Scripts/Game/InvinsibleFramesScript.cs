using System;
using UnityEngine;

namespace Game
{
    public class InvinsibleFramesScript : MonoBehaviour
    {
        float counter=0.0f;
        public bool started=false;
        public Action OnTimerEnd;

        public void FixedUpdate()
        {
            if(started)
            {
                counter += Time.deltaTime;
            }
            if (counter > 1.5f)
            {
                started = false;
                counter = 0.0f;
                OnTimerEnd?.Invoke();
            }
        }
        public void StartTimer()
        {
            started = true;
        }

    }
}
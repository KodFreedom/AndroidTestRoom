using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kodfreedom
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public TouchManager touchManager { get; private set; }
        public TouchObserver touchObserver { get; private set; }

        private void Awake()
        {
            if (!instance)
            {
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if(instance == this)
            {
                instance = null;
            }
        }

        private void Init()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            touchManager = GetComponent<TouchManager>();
            touchObserver = GetComponent<TouchObserver>();
        }
    }
}
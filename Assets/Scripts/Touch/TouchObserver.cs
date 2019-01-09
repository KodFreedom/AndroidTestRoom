using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kodfreedom
{
    public class TouchObserver : MonoBehaviour
    {
        public Vector2 touchCenter { get; private set; }
        private List<TouchListener> touchListeners = new List<TouchListener>();

        public void Register(TouchListener touchListener)
        {
            touchListeners.Add(touchListener);
        }

        public void Deregister(TouchListener touchListener)
        {
            touchListeners.Remove(touchListener);
        }

        private void Start()
        {
            touchCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }

        private void Update()
        {
            if (touchListeners.Count <= 0) return;
            var touchManager = GameManager.instance.touchManager;
            if (touchManager.fingers.Count <= 0) return;

            foreach(var finger in touchManager.fingers)
            {
                switch(finger.Value.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegin(finger.Value);
                        break;
                    case TouchPhase.Stationary:
                        OnTouchStationary(finger.Value);
                        break;
                    case TouchPhase.Moved:
                        OnTouchMove(finger.Value);
                        break;
                    case TouchPhase.Ended:
                        OnTouchEnd(finger.Value);
                        break;
                    case TouchPhase.Canceled:
                        OnTouchCancel(finger.Value);
                        break;
                }
            }
        }

        private void OnTouchBegin(Touch touch)
        {
            foreach(var touchListener in touchListeners)
            {
                touchListener.OnTouchBegin(touch);
            }
        }

        private void OnTouchStationary(Touch touch)
        {
            foreach (var touchListener in touchListeners)
            {
                touchListener.OnTouchStationary(touch);
            }
        }

        private void OnTouchMove(Touch touch)
        {
            foreach (var touchListener in touchListeners)
            {
                touchListener.OnTouchMove(touch);
            }
        }

        private void OnTouchEnd(Touch touch)
        {
            foreach (var touchListener in touchListeners)
            {
                touchListener.OnTouchEnd(touch);
            }
        }

        private void OnTouchCancel(Touch touch)
        {
            foreach (var touchListener in touchListeners)
            {
                touchListener.OnTouchCancel(touch);
            }
        }
    }
}
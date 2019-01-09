using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kodfreedom
{
    public class TouchManager : MonoBehaviour
    {
        public Dictionary<int, Touch> fingers { get; private set; }

        private void Start()
        {
            fingers = new Dictionary<int, Touch>();
        }

        private void Update()
        {
            UpdateFingers();

#if UNITY_EDITOR
            SimulateTouchWithMouse();
#endif
        }

        private void UpdateFingers()
        {
            if(fingers.Count > 0)
            {
                foreach (var finger in fingers)
                {
                    if (finger.Value.phase == TouchPhase.Ended
                        || finger.Value.phase == TouchPhase.Canceled)
                    {
                        fingers.Remove(finger.Key);
                        if (fingers.Count <= 0) break;
                    }
                }
            }

            foreach (var touch in Input.touches)
            {
                if (fingers.ContainsKey(touch.fingerId))
                {
                    fingers[touch.fingerId] = touch;
                }
                else
                {
                    fingers.Add(touch.fingerId, touch);
                }
            }
        }

#if UNITY_EDITOR
        private void SimulateTouchWithMouse()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var value = new Touch();
                value.phase = TouchPhase.Began;
                value.fingerId = -1;
                value.deltaPosition = Vector2.zero;
                value.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                fingers.Add(-1, value);
            }
            else if(Input.GetMouseButton(0))
            {
                var value = fingers[-1];
                value.phase = TouchPhase.Moved;
                var mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                value.deltaPosition = mousePosition - value.position;
                value.position = mousePosition;
                fingers[-1] = value;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var value = fingers[-1];
                value.phase = TouchPhase.Ended;
                fingers[-1] = value;
            }
            else if(fingers.ContainsKey(-1))
            {
                var value = fingers[-1];
                value.phase = TouchPhase.Canceled;
                fingers[-1] = value;
            }
        }
#endif
    }
}
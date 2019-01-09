using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kodfreedom
{
    public class TouchListener : MonoBehaviour
    {
        public virtual void OnTouchBegin(Touch touch) { }

        public virtual void OnTouchStationary(Touch touch) { }

        public virtual void OnTouchMove(Touch touch) { }

        public virtual void OnTouchEnd(Touch touch) { }

        public virtual void OnTouchCancel(Touch touch) { }

        protected virtual void Start()
        {
            GameManager.instance.touchObserver.Register(this);
        }

        protected virtual void OnDestroy()
        {
            if (GameManager.instance)
            {
                if (GameManager.instance.touchObserver)
                {
                    GameManager.instance.touchObserver.Deregister(this);
                }
            }
        }
    }
}
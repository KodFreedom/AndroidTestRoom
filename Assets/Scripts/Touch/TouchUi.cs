using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kodfreedom
{
    public class TouchUi : TouchListener
    {
        private Image m_ui;
        private int m_fingerId;
        private Vector2 m_origin;

        protected override void Start()
        {
            base.Start();
            m_ui = GetComponent<Image>();
            m_ui.enabled = false;
            m_fingerId = int.MinValue;
        }

        public override void OnTouchBegin(Touch touch)
        {
            if(m_fingerId == int.MinValue)
            {
                m_fingerId = touch.fingerId;
                m_origin = touch.position;
                m_ui.enabled = true;
                m_ui.transform.eulerAngles = Vector3.zero;
                m_ui.transform.localScale = Vector3.zero;
            }
        }

        public override void OnTouchStationary(Touch touch)
        {
            if (m_fingerId != touch.fingerId) return;
        }

        public override void OnTouchMove(Touch touch)
        {
            if (m_fingerId != touch.fingerId) return;
            var eulerAngles = Vector3.zero;
            var tangent = touch.position - m_origin;
            eulerAngles.z = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            m_ui.transform.eulerAngles = eulerAngles;
            m_ui.transform.position = (m_origin + touch.position) * 0.5f;
            m_ui.transform.localScale = new Vector3(tangent.magnitude / m_ui.transform.parent.localScale.x, 1f, 1f);
        }

        public override void OnTouchEnd(Touch touch)
        {
            if (m_fingerId != touch.fingerId) return;
            m_fingerId = int.MinValue;
            m_ui.enabled = false;
        }

        public override void OnTouchCancel(Touch touch)
        {
            if (m_fingerId != touch.fingerId) return;
            m_fingerId = int.MinValue;
            m_ui.enabled = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MZTATest.Controls
{
    public class UIStateCopyer : Selectable
    {
        private static PropertyInfo _currentSelectionStateInfo;

        static UIStateCopyer()
        {
            _currentSelectionStateInfo = typeof(Selectable).GetProperty("currentSelectionState", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [SerializeField]
        private Selectable _copyFrom;

        private void CopyState()
        {
            if (_copyFrom != null)
                DoStateTransition((SelectionState)_currentSelectionStateInfo.GetValue(_copyFrom), false);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            CopyState();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }
    }
}

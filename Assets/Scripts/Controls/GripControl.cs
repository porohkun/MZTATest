using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace MZTATest.Controls
{
    public class GripControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void GripHandler(GripControl sender);

        [SerializeField]
        private string _cursorName;

        public event GripHandler BeginGrip;
        public event GripHandler EndGrip;

        private CursorService _cursorService;

        [Inject]
        public void Construct(CursorService cursorService)
        {
            _cursorService = cursorService;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _cursorService.SetCursor(this, _cursorName, true);
            BeginGrip?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _cursorService.DropCursor(this, true);
            EndGrip?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _cursorService.SetCursor(this, _cursorName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorService.DropCursor(this);
        }
    }

    public class GripControlClickingWrapper
    {
        public event Action MouseDown;
        public event Action<Vector2> BeginGrip;
        public event Action EndGrip;
        public event Action Click;
        public event Action DoubleClick;

        private GripControl _grip;
        private float _sqrClickDistance;
        private float _doubleClickTime;

        private bool _clickWaiting;
        private Vector3 _startMousePos;
        private float _lastClickTime;

        public GripControlClickingWrapper(GripControl grip, float clickDistance, float doubleClickTime)
        {
            _grip = grip;
            _sqrClickDistance = clickDistance * clickDistance;
            _doubleClickTime = doubleClickTime;

            grip.BeginGrip += Grip_BeginGrip;
            grip.EndGrip += Grip_EndGrip;
        }

        private void Grip_BeginGrip(GripControl sender)
        {
            MouseDown?.Invoke();
            _clickWaiting = true;
            _startMousePos = Input.mousePosition;
        }

        private void Grip_EndGrip(GripControl sender)
        {
            if (!_clickWaiting)
                EndGrip?.Invoke();
            else
            {
                Click?.Invoke();
                _clickWaiting = false;
                var clickTime = Time.unscaledTime;
                if (clickTime - _lastClickTime < _doubleClickTime)
                    DoubleClick?.Invoke();
                _lastClickTime = clickTime;
            }
        }

        public void Update()
        {
            if (_clickWaiting)
            {
                var offset = Input.mousePosition - _startMousePos;
                if (offset.sqrMagnitude > _sqrClickDistance)
                {
                    _clickWaiting = false;
                    BeginGrip?.Invoke(offset);
                }
            }
        }
    }
}

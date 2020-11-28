using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

namespace MZTATest.Controls
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class MenuOpener : MonoBehaviour
    {
        public enum Direction
        {
            Horizontal,
            Vertical
        }

        [SerializeField]
        private RectTransform _menuTransform;
        [SerializeField]
        private Direction _direction;
        [SerializeField]
        private float _duration = 0.2f;

        public bool IsOpened { get; private set; }

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = transform as RectTransform;
            if (_menuTransform != null)
                CloseMenu(true);
        }

        private void Update()
        {
            if (Application.isPlaying)
                enabled = false;
            else
            if (_menuTransform != null)
                rectTransform.sizeDelta = _menuTransform.sizeDelta;
        }

        public void OpenMenu(bool instant = false)
        {
            IsOpened = true;
            if (instant)
                SetSize(1);
            else
                TweenFactory.Tween(this, 0f, 1f, _duration, TweenScaleFunctions.CubicEaseIn,
                p => SetSize(p.CurrentValue),
                null);
        }

        public void CloseMenu(bool instant = false)
        {
            IsOpened = false;
            if (instant)
                SetSize(0);
            else
                TweenFactory.Tween(this, 1f, 0f, _duration, TweenScaleFunctions.CubicEaseOut,
                    p => SetSize(p.CurrentValue),
                    null);
        }

        private void SetSize(float lerp)
        {
            switch (_direction)
            {
                case Direction.Horizontal:
                    rectTransform.sizeDelta = _menuTransform.sizeDelta * new Vector2(lerp, 1);
                    break;
                case Direction.Vertical:
                    rectTransform.sizeDelta = _menuTransform.sizeDelta * new Vector2(1, lerp);
                    break;
            }
        }
    }
}

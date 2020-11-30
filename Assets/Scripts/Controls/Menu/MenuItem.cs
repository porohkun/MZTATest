using MZTATest.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace MZTATest.Controls
{
    public class MenuItem : Selectable, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public delegate void ClickedHandler(MenuItem sender);

        public enum State
        {
            Normal = 0,
            Highlighted = 1,
            Pressed = 2,
            Selected = 3,
            Disabled = 4
        }

        [SerializeField]
        private MenuOpener _menuOpener;
        [SerializeField]
        private Menu _subMenu;

        public event ClickedHandler Clicked;

        public event PropertyChangedHandler<MenuItem, bool> IsPointerInsideChanged;
        private bool _isPointerInside;
        public bool IsPointerInside
        {
            get => _isPointerInside;
            set
            {
                if (_isPointerInside != value)
                {
                    _isPointerInside = value;
                    IsPointerInsideChanged?.Invoke(this, !value, value);
                }
            }
        }

        public bool HasSubMenu => _subMenu != null;
        public Menu SubMenu => _subMenu;

        private ICommand _command;

        protected override void Awake()
        {
            base.Awake();
            _command = GetComponent<ICommand>();
        }

        private void Update()
        {
            if (Application.isPlaying && _command != null)
                interactable = _command.CanExecute();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            IsPointerInside = true;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            IsPointerInside = false;
        }

        public void SetState(State state)
        {
            if (interactable)
                DoStateTransition((SelectionState)state, false);
            else
                DoStateTransition(currentSelectionState, false);
        }

        public void OpenSubMenu()
        {
            _menuOpener.OpenMenu();
        }

        public void CloseSubMenu()
        {
            _menuOpener.CloseMenu();
        }

        public void InvokeCommand()
        {
            _command?.Execute();
        }
    }
}

using MZTATest.Controls;
using MZTATest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MZTATest.Views
{
    public class BlockView : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<BlockViewModel, BlockView> { }

        [SerializeField]
        private Text _titleText;
        [SerializeField]
        private InputField _titleInputField;
        [SerializeField]
        private GripControl _titleGrip;
        [SerializeField]
        private Image _backgroundImage;
        [SerializeField]
        private GameObject _selection;

        [SerializeField]
        private GripControl[] _moveGrips;
        [SerializeField]
        private GripControl[] _topGrips;
        [SerializeField]
        private GripControl[] _bottomGrips;
        [SerializeField]
        private GripControl[] _leftGrips;
        [SerializeField]
        private GripControl[] _rightGrips;

        public event Action<Vector2> MovingBegins;
        public event Action MovingEnds;

        private BlockViewModel _viewModel;
        private RectTransform _rectTransform;
        private GripControlClickingWrapper[] _moveGripWrappers;
        private GripControlClickingWrapper _titleGripWrapper;
        private bool _titleEditingMode;

        [Inject]
        public void Construct(BlockViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Awake()
        {
            _rectTransform = transform as RectTransform;

            _titleGripWrapper = new GripControlClickingWrapper(_titleGrip, 20, 0.3f);
            _titleGripWrapper.DoubleClick += BeginEditTitle;

            foreach (var grip in _topGrips.Union(_bottomGrips).Union(_leftGrips).Union(_rightGrips))
            {
                grip.BeginGrip += BeginGrip;
                grip.EndGrip += EndGrip;
            }

            _moveGripWrappers = _moveGrips.Select(g => new GripControlClickingWrapper(g, 5, 0.3f)).ToArray();
            foreach (var gripWrapper in _moveGripWrappers)
            {
                gripWrapper.BeginGrip += (offset) =>
                {
                    if (!_viewModel.Selected) _viewModel.Select(IsShift());
                    MovingBegins?.Invoke(offset);
                };
                gripWrapper.EndGrip += () => MovingEnds?.Invoke();
                gripWrapper.Click += () => _viewModel.Select(IsShift());
            }
        }

        private bool IsShift() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        private void BeginGrip(GripControl sender)
        {
            _viewModel.BeginGrip(
                _topGrips.Contains(sender),
                _bottomGrips.Contains(sender),
                _leftGrips.Contains(sender),
                _rightGrips.Contains(sender),
                Input.mousePosition.ToVector2XY());
        }

        private void EndGrip(GripControl sender)
        {
            _viewModel.EndGrip();
        }

        private void BeginEditTitle()
        {
            {//HACK: снимает выделение со всех блоков чтобы del во время редактирования заголовка не удалял блоки.
                _viewModel.Select(false);
                if (_viewModel.Selected)
                    _viewModel.Select(false);
            }

            _titleEditingMode = true;
            _titleInputField.text = _viewModel.Title;
            _titleInputField.gameObject.SetActive(true);
            _titleInputField.Select();
            _titleInputField.ActivateInputField();
        }

        private void EndEditTitle(bool apply)
        {
            _titleEditingMode = false;
            if (apply)
                _viewModel.SetTitle(_titleInputField.text);
            _titleInputField.gameObject.SetActive(false);
        }

        private void Update()
        {
            foreach (var gripWrapper in _moveGripWrappers)
                gripWrapper.Update();

            if (_titleEditingMode)
            {
                if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) ||
                    ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !_titleInputField.gameObject.IsPointerOverUIObject()))
                    EndEditTitle(true);
                else if (Input.GetKeyUp(KeyCode.Escape))
                    EndEditTitle(false);
            }

            if (_selection.activeSelf != _viewModel.Selected)
                _selection.SetActive(_viewModel.Selected);
            _titleText.text = _viewModel.Title;
            _backgroundImage.color = _viewModel.Color;
            _rectTransform.sizeDelta = _viewModel.Size.Round();
            _rectTransform.anchoredPosition = _viewModel.Position.Round();

            if (_viewModel.IsGripping)
                _viewModel.ContinueGrip(Input.mousePosition.ToVector2XY());
        }
    }
}

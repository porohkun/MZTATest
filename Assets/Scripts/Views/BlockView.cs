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
        private Image _backgroundImage;
        [SerializeField]
        private GameObject _selection;

        [SerializeField]
        private GripControl _moveGrip;
        [SerializeField]
        private GripControl[] _topGrips;
        [SerializeField]
        private GripControl[] _bottomGrips;
        [SerializeField]
        private GripControl[] _leftGrips;
        [SerializeField]
        private GripControl[] _rightGrips;

        private BlockViewModel _viewModel;
        private RectTransform _rectTransform;
        private GripControlClickingWrapper _moveGripWrapper;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;

            foreach (var grip in _topGrips.Union(_bottomGrips).Union(_leftGrips).Union(_rightGrips))
            {
                grip.BeginGrip += BeginGrip;
                grip.EndGrip += EndGrip;
            }

            _moveGripWrapper = new GripControlClickingWrapper(_moveGrip, 5, 0.1f);
            _moveGripWrapper.BeginGrip += (offset) => _viewModel.BeginGrip(true, true, true, true, Input.mousePosition.ToVector2XY() - offset);
            _moveGripWrapper.EndGrip += () => _viewModel.EndGrip();
            _moveGripWrapper.Click += () => _viewModel.Select(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift));
        }

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

        [Inject]
        public void Construct(BlockViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Update()
        {
            _moveGripWrapper.Update();

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

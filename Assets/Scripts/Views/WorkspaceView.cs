using MZTATest.Controls;
using MZTATest.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;
using System.Collections.Generic;

namespace MZTATest.Views
{
    public class WorkspaceView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private GridController _grid;
        [SerializeField]
        private Text _offsetText;

        private WorkspaceViewModel _viewModel;
        private BlockView.Factory _blocksViewFactory;

        private Dictionary<BlockViewModel, BlockView> _blocks = new Dictionary<BlockViewModel, BlockView>();

        [Inject]
        public void Construct(WorkspaceViewModel viewModel, BlockView.Factory blocksViewFactory)
        {
            _viewModel = viewModel;
            _blocksViewFactory = blocksViewFactory;
            _viewModel.BlockAdded += _viewModel_BlockAdded;
            _viewModel.BlockRemoved += _viewModel_BlockRemoved;
            _viewModel.UpdateWorkspace();
        }

        private void _viewModel_BlockRemoved(BlockViewModel blockVM)
        {
            if (_blocks.TryGetValue(blockVM, out var view))
            {
                Destroy(view.gameObject);
                _blocks.Remove(blockVM);
            }
        }

        private void _viewModel_BlockAdded(BlockViewModel blockVM)
        {
            var view = _blocksViewFactory.Create(blockVM);
            view.transform.SetParent(transform);
            view.MovingBegins += View_MovingBegins;
            view.MovingEnds += View_MovingEnds;
            _blocks.Add(blockVM, view);
        }

        private void View_MovingBegins(Vector2 offset)
        {
            _viewModel.BeginMoveSelectedBlocks(Input.mousePosition.ToVector2XY() - offset);
        }

        private void View_MovingEnds()
        {
            _viewModel.EndMoveSelectedBlocks();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _viewModel.BeginScroll(Input.mousePosition.ToVector2XY());
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _viewModel.EndScroll();
            }
        }

        private void Update()
        {
            if (_viewModel.IsScrolling)
                _viewModel.ContinueScrolling(Input.mousePosition.ToVector2XY());

            _grid.Offset = _viewModel.Offset.Round();
            _offsetText.text = _viewModel.Offset.Round().ToString();
        }
    }
}

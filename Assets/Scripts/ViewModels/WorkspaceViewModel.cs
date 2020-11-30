using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZTATest.Models;
using MZTATest.Services;
using UnityEngine;

namespace MZTATest.ViewModels
{
    public class WorkspaceViewModel
    {
        WorkspaceKeeperService _workspaceService;
        BlockViewModel.Factory _blocksVMFactory;

        public Vector2 Offset { get; private set; }
        public bool IsScrolling { get; private set; }

        public event Action<BlockViewModel> BlockAdded;
        public event Action<BlockViewModel> BlockRemoved;

        private Workspace _workspace;
        private List<BlockViewModel> _blocks = new List<BlockViewModel>();
        private Vector2 _scrollStartMousePos;
        private Vector2 _scrollStartOffset;


        public WorkspaceViewModel(WorkspaceKeeperService workspaceService, BlockViewModel.Factory blocksVMFactory)
        {
            _workspaceService = workspaceService;
            _blocksVMFactory = blocksVMFactory;

            _workspaceService.CurrentWorkspaceChanged += UpdateWorkspace;
        }

        public void UpdateWorkspace()
        {
            if (_workspace != null)
                foreach (var block in _workspace.Blocks)
                    _workspace_BlockRemoved(block);
            _workspace = _workspaceService.GetWorkspace();
            _workspace.BlockAdded += _workspace_BlockAdded;
            _workspace.BlockRemoved += _workspace_BlockRemoved;
            foreach (var block in _workspace.Blocks)
                _workspace_BlockAdded(block);
        }

        private void _workspace_BlockAdded(Block block)
        {
            var blockVM = _blocksVMFactory.Create(block);
            blockVM.Offset = Offset;
            _blocks.Add(blockVM);
            BlockAdded?.Invoke(blockVM);
        }

        private void _workspace_BlockRemoved(Block block)
        {
            var blockVM = _blocks.Find(b => b.IsBlock(block));
            if (blockVM != null)
            {
                BlockRemoved?.Invoke(blockVM);
                _blocks.Remove(blockVM);
            }
        }

        public void BeginScroll(Vector2 position)
        {
            IsScrolling = true;
            _scrollStartMousePos = position;
            _scrollStartOffset = Offset;
        }

        public void ContinueScrolling(Vector2 position)
        {
            Offset = _scrollStartOffset - position + _scrollStartMousePos;
            foreach (var block in _blocks)
                block.Offset = Offset;
        }

        public void EndScroll()
        {
            IsScrolling = false;
        }

        public void BeginMoveSelectedBlocks(Vector2 position)
        {
            foreach (var block in _blocks.Where(b => b.Selected))
                block.BeginGrip(true, true, true, true, position);

        }

        public void EndMoveSelectedBlocks()
        {
            foreach (var block in _blocks.Where(b => b.Selected))
                block.EndGrip();
        }
    }
}

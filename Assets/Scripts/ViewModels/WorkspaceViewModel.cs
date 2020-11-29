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
        private Vector2 _scrollStartMousePos;
        private Vector2 _scrollStartOffset;
        private List<BlockViewModel> _blocks = new List<BlockViewModel>();

        public WorkspaceViewModel(WorkspaceKeeperService workspaceService, BlockViewModel.Factory blocksVMFactory)
        {
            _workspaceService = workspaceService;
            _blocksVMFactory = blocksVMFactory;

            _workspaceService.CurrentWorkspaceChanged += UpdateWorkspace;
        }

        public void UpdateWorkspace()
        {
            _workspace = _workspaceService.GetWorkspace();
            LocateBlocks();
        }

        private void LocateBlocks()
        {
            foreach (var block in _blocks)
                BlockRemoved?.Invoke(block);
            _blocks.Clear();
            _blocks.AddRange(_workspace.Blocks.Select(b => _blocksVMFactory.Create(b)));
            foreach (var block in _blocks)
                BlockAdded?.Invoke(block);
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

    }
}

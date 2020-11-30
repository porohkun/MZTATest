using MZTATest.Services;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MZTATest.Commands
{
    public class DeleteSelectedBlocksCommand : MonoBehaviourCommand
    {
        private BlocksSelectionService _blocksSelectionService;
        private WorkspaceKeeperService _workspaceKeeperService;

        [Inject]
        public void Construct(BlocksSelectionService blocksSelectionService, WorkspaceKeeperService workspaceKeeperService)
        {
            _blocksSelectionService = blocksSelectionService;
            _workspaceKeeperService = workspaceKeeperService;
        }
        public override bool CanExecute()
        {
            return _blocksSelectionService?.IsAnyBlockSelected() ?? false;
        }

        public override void Execute()
        {
            var ws = _workspaceKeeperService.GetWorkspace();
            foreach (var block in ws.Blocks.Where(b => _blocksSelectionService.IsBlockSelected(b)).ToArray())
            {
                ws.RemoveBlock(block);
                _blocksSelectionService.Deselect(block);
            }
        }
    }
}

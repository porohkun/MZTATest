using MZTATest.Services;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MZTATest.Commands
{
    public class NewBlockCommand : MonoBehaviourCommand
    {
        private BlocksSelectionService _blocksSelectionService;
        private WorkspaceKeeperService _workspaceKeeperService;
        private ColorPickerService _colorPickerService;
        private WorkspaceScrollService _workspaceScrollService;

        [Inject]
        public void Construct(BlocksSelectionService blocksSelectionService, WorkspaceKeeperService workspaceKeeperService, ColorPickerService colorPickerService, WorkspaceScrollService workspaceScrollService)
        {
            _blocksSelectionService = blocksSelectionService;
            _workspaceKeeperService = workspaceKeeperService;
            _colorPickerService = colorPickerService;
            _workspaceScrollService = workspaceScrollService;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            var ws = _workspaceKeeperService.GetWorkspace();
            var block = new Models.Block()
            {
                Color = _colorPickerService.GetRandomColor()
            };
            block.Position = (_workspaceScrollService.WindowSize - block.Size) / 2f + _workspaceScrollService.Offset;
            ws.AddBlock(block);
        }
    }
}

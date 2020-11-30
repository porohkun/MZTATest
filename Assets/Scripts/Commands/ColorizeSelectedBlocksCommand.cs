using MZTATest.Services;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MZTATest.Commands
{
    public class ColorizeSelectedBlocksCommand : MonoBehaviourCommand
    {
        private BlocksSelectionService _blocksSelectionService;
        private WorkspaceKeeperService _workspaceKeeperService;
        private ColorPickerService _colorPickerService;

        [Inject]
        public void Construct(BlocksSelectionService blocksSelectionService, WorkspaceKeeperService workspaceKeeperService, ColorPickerService colorPickerService)
        {
            _blocksSelectionService = blocksSelectionService;
            _workspaceKeeperService = workspaceKeeperService;
            _colorPickerService = colorPickerService;
        }

        public override bool CanExecute()
        {
            return _blocksSelectionService?.IsAnyBlockSelected() ?? false;
        }

        public override void Execute()
        {
            _colorPickerService.ShowColorPicker(Colorize);
        }

        private void Colorize(Color color)
        {
            var ws = _workspaceKeeperService.GetWorkspace();
            foreach (var block in ws.Blocks.Where(b => _blocksSelectionService.IsBlockSelected(b)).ToArray())
            {
                block.Color = color;
            }
        }
    }
}

using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace MZTATest.Commands
{
    public class SaveAsWorkspaceCommand : MonoBehaviourCommand
    {
        private WorkspaceKeeperService _workspaceKeeperService;
        private ISaveLoadService _saveLoadService;
        private ISerializationService _serializationService;

        [Inject]
        public void Construct(WorkspaceKeeperService workspaceKeeperService, ISaveLoadService saveLoadService, ISerializationService serializationService)
        {
            _workspaceKeeperService = workspaceKeeperService;
            _saveLoadService = saveLoadService;
            _serializationService = serializationService;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            var ws = _workspaceKeeperService.GetWorkspace();

            var data = _serializationService.Serialize(ws);
            if (_saveLoadService.SaveFile(data, out var path))
            {
                ws.Path = path;
                ws.Edited = false;
            }
        }
    }
}

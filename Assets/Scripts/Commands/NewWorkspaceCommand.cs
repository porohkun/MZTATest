using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace MZTATest.Commands
{
    public class NewWorkspaceCommand : MonoBehaviourCommand
    {
        private WorkspaceKeeperService _workspaceKeeperService;
        private MessageBoxService _messageBoxService;
        private ISaveLoadService _saveLoadService;
        private ISerializationService _serializationService;

        [Inject]
        public void Construct(WorkspaceKeeperService workspaceKeeperService, MessageBoxService messageBoxService, ISaveLoadService saveLoadService, ISerializationService serializationService)
        {
            _workspaceKeeperService = workspaceKeeperService;
            _messageBoxService = messageBoxService;
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
            if (ws.Edited)
                _messageBoxService.ShowYesNoCancelMessageBox("", "", SaveAndCreate, Create);
        }

        private void SaveAndCreate()
        {
            if (Save())
                Create();
        }

        private void Create()
        {
            _workspaceKeeperService.CreateWorkspace();
        }

        private bool Save()
        {
            var ws = _workspaceKeeperService.GetWorkspace();

            var data = _serializationService.Serialize(ws);
            var path = ws.Path;
            bool saved;
            if (string.IsNullOrWhiteSpace(ws.Path))
                saved = _saveLoadService.SaveFile(data, out path);
            else
                saved = _saveLoadService.SaveFileTo(data, path);
            if (saved)
                ws.Path = path;
            return saved;
        }
    }
}

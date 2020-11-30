using MZTATest.Models;
using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace MZTATest.Commands
{
    public class OpenWorkspaceCommand : MonoBehaviourCommand
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
                _messageBoxService.ShowYesNoCancelMessageBox("Внимание!", "Текущий файл не сохранен. Сохранить его перед тем, как открыть другой?", SaveAndOpen, Open);
            else
                Open();
        }

        private void SaveAndOpen()
        {
            if (Save())
                Open();
        }

        private void Open()
        {
            _saveLoadService.OpenFile(FileOpened);
        }

        private void FileOpened(string data)
        {
            var ws = _serializationService.Deserialize<Workspace>(data);

            _workspaceKeeperService.SetWorkspace(ws);
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

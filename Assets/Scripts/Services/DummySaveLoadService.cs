using System;
using UnityEngine;
using Zenject;

namespace MZTATest.Services
{
    public class DummySaveLoadService : MonoBehaviour, ISaveLoadService
    {
        private MessageBoxService _messageBoxService;

        [Inject]
        public void Construct(MessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }

        public void OpenFile(Action<string> callback)
        {
            _messageBoxService.ShowMessageBox("Внимание!", "Работа с файлами поддерживается только в Windows и WebGL версиях", null, ("OK", null));
        }

        public bool SaveFile(string data, out string path)
        {
            path = null;
            _messageBoxService.ShowMessageBox("Внимание!", "Работа с файлами поддерживается только в Windows и WebGL версиях", null, ("OK", null));
            return false;
        }

        public bool SaveFileTo(string data, string path)
        {
            _messageBoxService.ShowMessageBox("Внимание!", "Работа с файлами поддерживается только в Windows и WebGL версиях", null, ("OK", null));
            return false;
        }
    }
}

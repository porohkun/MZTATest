using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Services
{
    public class WindowsSaveLoadService : MonoBehaviour, ISaveLoadService
    {
        private Action<string> _callback;

        public void OpenFile(Action<string> callback)
        {
            _callback = callback;
            var paths = StandaloneFileBrowser.OpenFilePanel("Открыть файл", "", "json", false);
            if (paths.Length > 0)
            {
                StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
            }
        }

        private IEnumerator OutputRoutine(string url)
        {
            var loader = new WWW(url);
            yield return loader;
            _callback?.Invoke(loader.text);
            _callback = null;
        }

        public bool SaveFile(string data, out string path)
        {
            path = StandaloneFileBrowser.SaveFilePanel("Сохранить файл", "", "sample", "json");
            return SaveFileTo(data, path);
        }

        public bool SaveFileTo(string data, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, data);
                return true;
            }
            return false;
        }
    }
}

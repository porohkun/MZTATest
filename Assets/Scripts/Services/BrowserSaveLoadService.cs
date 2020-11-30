using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Services
{
    public class BrowserSaveLoadService : MonoBehaviour, ISaveLoadService
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
        [DllImport("__Internal")]
        private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);
#else
        private static void UploadFile(string gameObjectName, string methodName, string filter, bool multiple) { }
        private static void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize) { }
#endif

        private Action<string> _callback;

        public void OpenFile(Action<string> callback)
        {
            _callback = callback;
            UploadFile(gameObject.name, nameof(OnFileUpload), ".json", false);
        }

        // Called from browser
        public void OnFileUpload(string url)
        {
            StartCoroutine(OutputRoutine(url));
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
            var bytes = Encoding.UTF8.GetBytes(data);
            DownloadFile(gameObject.name, nameof(OnFileDownload), "workspace.json", bytes, bytes.Length);
            path = null;
            return true;
        }

        // Called from browser
        public void OnFileDownload(string url)
        {

        }

        public bool SaveFileTo(string data, string path)
        {
            return SaveFile(data, out var _);
        }
    }
}

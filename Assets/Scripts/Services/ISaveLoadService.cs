using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZTATest.Services
{
    public interface ISaveLoadService
    {
        void OpenFile(Action<string> callback);
        bool SaveFile(string data, out string path);
        bool SaveFileTo(string data, string path);
    }
}

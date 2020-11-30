using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZTATest.Services
{
    public interface ISerializationService
    {
        string Serialize(object data);
        T Deserialize<T>(string data);
    }
}

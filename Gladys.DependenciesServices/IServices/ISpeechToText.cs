using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.DependenciesServices.IServices
{
    public interface ISpeechToText
    {
        Task<string> Reconnaissance();
    }
}

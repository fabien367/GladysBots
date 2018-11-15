using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gladys.Services.IServices
{
   public interface IGetRessources
    {
         Stream GetStream(string Path);
    }
}

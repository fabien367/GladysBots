using Gladys.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Gladys.Services.Services
{
    public class GetRessources : IGetRessources
    {
        private readonly Assembly _assembly;
        public GetRessources()
        {
            _assembly = IntrospectionExtensions.GetTypeInfo(typeof(GetRessources)).Assembly;
        }
        /// <summary>
        /// Récupère le fichier ressource de la dll
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Stream GetStream(string Path)
        {
            Stream stream = _assembly.GetManifestResourceStream(Path);
            return stream;
        }
    }
}

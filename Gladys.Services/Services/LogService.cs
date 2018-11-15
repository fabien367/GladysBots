using Gladys.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Gladys.Services.Services
{
    public class LogService : ILogService
    {
        protected static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string filename = Path.Combine(path, "log.txt");
        public static void WriteLog(Exception ex)
        {
            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
                streamWriter.WriteLine("Erreur: "+ ex.StackTrace);
            }
        }
        public static void WriteLog(string ex)
        {
            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
                streamWriter.WriteLine("Erreur: " + ex);
            }
        }
        public static void WriteLog(HttpResponseMessage ex)
        {
            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
                streamWriter.WriteLine("Erreur: " + ex.StatusCode);
                streamWriter.WriteLine("Erreur: " + ex.ReasonPhrase);
                streamWriter.WriteLine("Erreur: " + ex.RequestMessage);
                streamWriter.WriteLine("Erreur: " + ex.RequestMessage.RequestUri.ToString());
            }
        }
    }
}

using Gladys.DependenciesServices.IServices;
using Gladys.UWP.DependenciesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly:Dependency(typeof(SpeechToText))]
namespace Gladys.UWP.DependenciesServices
{
    public class SpeechToText : ISpeechToText
    {
        public async Task<string> Reconnaissance()
        {
            try
            {
                // Create an instance of SpeechRecognizer.
                var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();
                // Compile the dictation grammar by default.
                await speechRecognizer.CompileConstraintsAsync();
                // Start recognition.
                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
                if (speechRecognitionResult.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {
                    return speechRecognitionResult.Text;
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
           
        }
    }
}

using Gladys.DependenciesServices.IServices;
using Gladys.UWP.DependenciesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeech))]
namespace Gladys.UWP.DependenciesServices
{
    public class TextToSpeech : ITextToSpeech
    {
        public async Task SpeakAsync(string text)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            var voices = SpeechSynthesizer.AllVoices;
            foreach (var item in voices)
            {
                if (item.Language == "fr-FR")
                    synth.Voice = item;
            }
            var speach = await synth.SynthesizeTextToStreamAsync(text);
            MediaElement mediaElement = new MediaElement();
            // Send the stream to the media object.
            mediaElement.SetSource(speach, speach.ContentType);
            mediaElement.Play();
        }
    }
}

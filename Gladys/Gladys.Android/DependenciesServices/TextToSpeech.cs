using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using Gladys.DependenciesServices.IServices;
using Gladys.Droid.DependenciesServices;
using Xamarin.Forms;
using static Android.Speech.Tts.TextToSpeech;

[assembly: Dependency(typeof(TextToSpeechImplementation))]
namespace Gladys.Droid.DependenciesServices
{
    public class TextToSpeechImplementation : Java.Lang.Object, ITextToSpeech, IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }

        public async Task SpeakAsync(string text)
        {
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);
                speaker.SetSpeechRate(0.75f);
            }
            else
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gladys.Models.MainPageModel
{
    public class SpeechModel
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public TextAlignment Align { get; set; }
        public bool IsUser { get; set; }
        public LayoutOptions Horizontal {get;set; }
        public int Index { get; set; }
    }
}

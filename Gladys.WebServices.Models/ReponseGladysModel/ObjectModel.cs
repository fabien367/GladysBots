using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.WebServices.Models.ReponseGladysModel
{
    public class ObjectModel
    {
        public ActionEnumModel TypeAction { get; set; }
        public string Reponse { get; set; }
        public List<string> ActionList { get; set; } = new List<string>();
    }

    public enum ActionEnumModel { None=0,Reponse=1,Action=2,ListAction=3}
}

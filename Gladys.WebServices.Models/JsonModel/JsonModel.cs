using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.WebServices.Models.JsonModel
{
    public class JsonModel
    {
        public string Intent { get; set; }
        public string Reponse { get; set; }
        public ActionEnumModel Action { get; set; }
    }
    public enum ActionEnumModel { None = 0, Reponse = 1, Action = 2, ListAction = 3 }
}

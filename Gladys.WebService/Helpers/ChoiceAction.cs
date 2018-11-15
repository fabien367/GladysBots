using Gladys.WebService.Controllers;
using Gladys.WebServices.Models;
using Gladys.WebServices.Models.JsonModel;
using Gladys.WebServices.Models.ReponseGladysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gladys.WebService.Helpers
{
    public class ChoiceAction
    {
        private readonly List<JsonModel> ListJsonModel;
        public ChoiceAction(List<JsonModel> jsonmodels)
        {
            ListJsonModel = jsonmodels;
        }

        public ObjectModel Process(LuisResult luis )
        {
          var test=  GetAction(luis);
            //luis.TopScoringIntent.Intent
            return new ObjectModel();
        }

        private WebServices.Models.JsonModel.ActionEnumModel GetAction(LuisResult luis)
        {
            return (WebServices.Models.JsonModel.ActionEnumModel) ListJsonModel.Where(o => o.Intent.Contains(luis.TopScoringIntent.Intent)).FirstOrDefault().Action;
        }
    }
}
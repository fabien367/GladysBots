using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galdys.WebServices.Luis
{
    /// <summary>
    /// LUIS intent recommendation. Look at https://www.luis.ai/Help for more
    /// information.
    /// </summary>
    public partial class IntentRecommendation
    {
        /// <summary>
        /// Initializes a new instance of the IntentRecommendation class.
        /// </summary>
        public IntentRecommendation() { }

        /// <summary>
        /// Initializes a new instance of the IntentRecommendation class.
        /// </summary>
        public IntentRecommendation(string intent = default(string), double? score = default(double?), IList<Action> actions = default(IList<Action>))
        {
            Intent = intent;
            Score = score;
            Actions = actions;
        }

        /// <summary>
        /// The LUIS intent detected by LUIS service in response to a query.
        /// </summary>
        [JsonProperty(PropertyName = "intent")]
        public string Intent { get; set; }

        /// <summary>
        /// The score for the detected intent.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

        /// <summary>
        /// The action associated with this Luis intent.
        /// </summary>
        [JsonProperty(PropertyName = "actions")]
        public IList<Action> Actions { get; set; }

    }
}

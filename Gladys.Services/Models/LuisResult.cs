using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gladys.Services.Models
{
    public class LuisResult
    {
        /// <summary>
        /// Initializes a new instance of the LuisResult class.
        /// </summary>
        public LuisResult() { }

        /// <summary>
        /// Initializes a new instance of the LuisResult class.
        /// </summary>
        public LuisResult(string query, IList<EntityRecommendation> entities, IntentRecommendation topScoringIntent = default(IntentRecommendation), IList<IntentRecommendation> intents = default(IList<IntentRecommendation>), IList<CompositeEntity> compositeEntities = default(IList<CompositeEntity>), string alteredQuery = default(string))
        {
            Query = query;
            TopScoringIntent = topScoringIntent;
            Intents = intents;
            Entities = entities;
            CompositeEntities = compositeEntities;
            AlteredQuery = alteredQuery;
        }

        /// <summary>
        /// The query sent to LUIS.
        /// </summary>
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "topScoringIntent")]
        public IntentRecommendation TopScoringIntent { get; set; }

        /// <summary>
        /// The intents found in the query text.
        /// </summary>
        [JsonProperty(PropertyName = "intents")]
        public IList<IntentRecommendation> Intents { get; set; } = Array.Empty<IntentRecommendation>();

        /// <summary>
        /// The entities found in the query text.
        /// </summary>
        [JsonProperty(PropertyName = "entities")]
        public IList<EntityRecommendation> Entities { get; set; } = Array.Empty<EntityRecommendation>();

        /// <summary>
        /// The composite entities found in the utterance.
        /// </summary>
        [JsonProperty(PropertyName = "compositeEntities")]
        public IList<CompositeEntity> CompositeEntities { get; set; } = Array.Empty<CompositeEntity>();

        /// <summary>
        /// The altered query used by LUIS to extract intent and entities. For
        /// example, when Bing spell check is enabled for a model, this field
        /// will contain the spell checked utterance.
        /// </summary>
        [JsonProperty(PropertyName = "alteredQuery")]
        public string AlteredQuery { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Query == null)
            {
                throw new ValidationException(ValidationResult.Success.ErrorMessage);
            }
            if (Entities == null)
            {
                throw new ValidationException(ValidationResult.Success.ErrorMessage);
            }
            if (this.Entities != null)
            {
                foreach (var element in this.Entities)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (this.CompositeEntities != null)
            {
                foreach (var element1 in this.CompositeEntities)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
        }
    }
}

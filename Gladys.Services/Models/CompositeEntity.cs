using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gladys.Services.Models
{
    /// <summary>
    /// Luis composite entity. Look at https://www.luis.ai/Help for more
    /// information.
    /// </summary>
    public partial class CompositeEntity
    {
        /// <summary>
        /// Initializes a new instance of the CompositeEntity class.
        /// </summary>
        public CompositeEntity() { }

        /// <summary>
        /// Initializes a new instance of the CompositeEntity class.
        /// </summary>
        public CompositeEntity(string parentType, string value, IList<CompositeChild> children)
        {
            ParentType = parentType;
            Value = value;
            Children = children;
        }

        /// <summary>
        /// Type of parent entity.
        /// </summary>
        [JsonProperty(PropertyName = "parentType")]
        public string ParentType { get; set; }

        /// <summary>
        /// Value for entity extracted by LUIS.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "children")]
        public IList<CompositeChild> Children { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (ParentType == null)
            {
                throw new ValidationException(ValidationResult.Success.ErrorMessage);
            }
            if (Value == null)
            {
                throw new ValidationException(ValidationResult.Success.ErrorMessage);
            }
            if (Children == null)
            {
                throw new ValidationException(ValidationResult.Success.ErrorMessage);
            }
            if (this.Children != null)
            {
                foreach (var element in this.Children)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}

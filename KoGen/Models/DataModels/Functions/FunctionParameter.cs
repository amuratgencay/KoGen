using KoGen.Extentions;
using static KoGen.Extentions.StringExtentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.DataModels.Functions
{
    [Serializable]
    public class FunctionParameter
    {
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public string Name { get; set; }
        public Class Type { get; set; }
        public object Value { get; set; }

        public readonly object DefaultValue;

        public FunctionParameter(string name, Class type, object value = null)
        {
            Name = name;
            Type = type;

            if (value == null && !type.Nullable)
                Value = type.DefaultValue;
            else if (value != null)
                Value = value;

            DefaultValue = Value;
        }

        private string AnnotationsString =>
            Annotations
            .Aggregate(x => x.ToString(), NewLine, NewLine, NewLine);
        private string GenericString =>
            Type.GenericList
            .Aggregate(x => x.Name, ", ", "<", ">");
        public override string ToString()
        {
            return $"{AnnotationsString}{Type.Name}{GenericString} {Name}";
        }
    }
}

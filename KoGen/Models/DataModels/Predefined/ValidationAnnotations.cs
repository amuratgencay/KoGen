using KoGen.Models.DataModels;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System.Collections.Generic;
using KoGen.Models.DataModels.Enum;
using KoGen.Models.ClassMembers;

namespace KoGen.Models.DataModels.Predefined
{
    public class ValidationAnnotations
    {
        public static Annotation Size() => new Annotation
        {
            Name = "Size",
            Package = "javax.validation.constraints.Size",
            Parameters = new Dictionary<string, ClassMember>
            {
                {"min", new ClassMember("min", JavaIntPrimitive, AccessModifier.Public) },
                {"max", new ClassMember("max", JavaIntPrimitive, AccessModifier.Public) },
            }
        };

        public static Annotation NotNull() => new Annotation
        {
            Name = "NotNull",
            Package = "javax.validation.constraints.NotNull",
        };
    }

}

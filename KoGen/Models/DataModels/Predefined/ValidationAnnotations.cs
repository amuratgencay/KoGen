using KoGen.Models.DataModels;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System.Collections.Generic;
using KoGen.Models.DataModels.Enum;
using System;

namespace KoGen.Models.DataModels.Predefined
{
    [Serializable]
    public class ValidationAnnotations
    {
        public static Annotation Size() => new Annotation("Size", "javax.validation.constraints.Size",
            new ClassMember("min", JavaIntPrimitive),
            new ClassMember("max", JavaIntPrimitive)
            );

        public static Annotation Digits() => new Annotation("Digits", "javax.validation.constraints.Digits",
            new ClassMember("integer", JavaIntPrimitive),
            new ClassMember("fraction", JavaIntPrimitive)
        );

        public static Annotation NotNull() => new Annotation("NotNull", "javax.validation.constraints.NotNull");
    }

}

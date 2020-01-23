using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System.Collections.Generic;
using KoGen.Models.ClassMembers;
using KoGen.Models.DataModels.Enum;

namespace KoGen.Models.DataModels.Predefined
{
    public class PredefinedAnnotations
    {
        public static Annotation Entity() => new Annotation
        {
            Name = "Entity",
            Package = "javax.persistence.Entity",
        };

        public static Annotation Column() => new Annotation
        {
            Name = "Column",
            Package = "javax.persistence.Column",
            Parameters = new Dictionary<string, ClassMember>
            {
           {     "name",
                new ClassMember("name", JavaString, AccessModifier.Public)
            },
            { "nullable", new ClassMember("nullable", JavaBooleanPrimitive, true, AccessModifier.Public) },
            { "insertable", new ClassMember("insertable", JavaBooleanPrimitive, true, AccessModifier.Public) },
            { "updatable", new ClassMember("updatable", JavaBooleanPrimitive, true, AccessModifier.Public) },

        }
        };

        public static Annotation Index() => new Annotation
        {
            Name = "Index",
            Package = "javax.persistence.Index",
            Parameters = new Dictionary<string, ClassMember>
            {
                { "name", new ClassMember("name", JavaString, AccessModifier.Public) },
                { "columnList", new ClassMember("columnList", JavaList, AccessModifier.Public) },
                { "unique", new ClassMember("unique", JavaBoolean, false, AccessModifier.Public) },
            }
        };

        public static Annotation Table() => new Annotation
        {
            Name = "Table",
            Package = "javax.persistence.Table",
            Parameters = new Dictionary<string, ClassMember>
            {
               {"name", new ClassMember("name", JavaString, AccessModifier.Public) },
               {"schema", new ClassMember("schema", JavaString, AccessModifier.Public) },
               {"indexes", new ClassMember("indexes", JavaList, AccessModifier.Public) }
            }
        };

        public static Annotation HvlEntitySequence() => new Annotation
        {
            Name = "HvlEntitySequence",
            Package = "tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence",
            Parameters = new Dictionary<string, ClassMember>
            {
                {"name", new ClassMember("name", JavaString, AccessModifier.Public) }
            }
        };


    }

}

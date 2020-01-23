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
                new ClassMember("name", JavaString)
            },
            { "nullable", new ClassMember("nullable", JavaBooleanPrimitive, true) },
            { "insertable", new ClassMember("insertable", JavaBooleanPrimitive, true) },
            { "updatable", new ClassMember("updatable", JavaBooleanPrimitive, true) },

        }
        };

        public static Annotation Index() => new Annotation
        {
            Name = "Index",
            Package = "javax.persistence.Index",
            Parameters = new Dictionary<string, ClassMember>
            {
                { "name", new ClassMember("name", JavaString) },
                { "columnList", new ClassMember("columnList", JavaList) },
                { "unique", new ClassMember("unique", JavaBoolean, false) },
            }
        };

        public static Annotation Table() => new Annotation
        {
            Name = "Table",
            Package = "javax.persistence.Table",
            Parameters = new Dictionary<string, ClassMember>
            {
               {"name", new ClassMember("name", JavaString) },
               {"schema", new ClassMember("schema", JavaString) },
               {"indexes", new ClassMember("indexes", JavaList) }
            }
        };

        public static Annotation HvlEntitySequence() => new Annotation
        {
            Name = "HvlEntitySequence",
            Package = "tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence",
            Parameters = new Dictionary<string, ClassMember>
            {
                {"name", new ClassMember("name", JavaString) }
            }
        };


    }

}

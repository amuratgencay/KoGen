using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.DataModels
{
    public class Annotation
    {
        public Package Package { get; set; }
        public string Name { get; set; }
        public Dictionary<string, DataModel> Parameters { get; set; } = new Dictionary<string, DataModel>();

        public override string ToString()
        {
            var res = $"@{Name}";
            if (Parameters.Count > 0)
            {
                res += "(";
                foreach (var key in Parameters.Keys)
                {
                    res += key + " = " + Parameters[key].AssingString() + ", ";
                }
                res = res.Remove(res.Length - 2);
                res += ")";
            }

            return res;
        }
    }
    public class DataAnnotations
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
            Parameters = new Dictionary<string, DataModel>
            {
           {     "name",
                new DataModel("name", typeof(string), AccessModifier.Public)
            },
            { "nullable", new DataModel("nullable", typeof(bool), true, AccessModifier.Public) },
            { "insertable", new DataModel("insertable", typeof(bool), true, AccessModifier.Public) },
            { "updatable", new DataModel("updatable", typeof(bool), true, AccessModifier.Public) },

        }
        };

        public static Annotation Index() => new Annotation
        {
            Name = "Index",
            Package = "javax.persistence.Index",
            Parameters = new Dictionary<string, DataModel>
            {
                { "name", new DataModel("name", typeof(string), AccessModifier.Public) },
                { "columnList", new DataModel("columnList", typeof(string), AccessModifier.Public) },
                { "unique", new DataModel("unique", typeof(bool), false, AccessModifier.Public) },
            }
        };

        public static Annotation Table() => new Annotation
        {
            Name = "Table",
            Package = "javax.persistence.Table",
            Parameters = new Dictionary<string, DataModel>
            {
               {"name", new DataModel("name", typeof(string), AccessModifier.Public) },
               {"schema", new DataModel("schema", typeof(string), AccessModifier.Public) },
               {"indexes", new DataModel("indexes", typeof(List<Annotation>), AccessModifier.Public) }
            }
        };

        public static Annotation HvlEntitySequence() => new Annotation
        {
            Name = "HvlEntitySequence",
            Package = "tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence",
            Parameters = new Dictionary<string, DataModel>
            {
                {"name", new DataModel("name", typeof(string), AccessModifier.Public) }
            }
        };


    }
    public class ValidationAnnotations
    {
        public static Annotation Size() => new Annotation
        {
            Name = "Size",
            Package = "javax.validation.constraints.Size",
            Parameters = new Dictionary<string, DataModel>
            {
                {"min", new DataModel("min", typeof(int), AccessModifier.Public) },
                {"max", new DataModel("max", typeof(int), AccessModifier.Public) },
            }
        };

        public static Annotation NotNull() => new Annotation
        {
            Name = "NotNull",
            Package = "javax.validation.constraints.NotNull",
        };
    }

}

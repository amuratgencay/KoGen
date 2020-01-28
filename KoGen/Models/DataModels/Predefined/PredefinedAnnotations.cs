using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System.Collections.Generic;
using KoGen.Models.ClassMembers;
using KoGen.Models.DataModels.Enum;

namespace KoGen.Models.DataModels.Predefined
{
    public class PredefinedAnnotations
    {

        public static Annotation Entity() => new Annotation("Entity", "javax.persistence.Entity");

        public static Annotation Column() => new Annotation("Column", "javax.persistence.Column",
            new ClassMember("name", JavaString),
            new ClassMember("nullable", JavaBooleanPrimitive, true),
            new ClassMember("insertable", JavaBooleanPrimitive, true),
            new ClassMember("updatable", JavaBooleanPrimitive, true)
        );

        public static Annotation Index() => new Annotation("Index", "javax.persistence.Index",
            new ClassMember("name", JavaString),
            new ClassMember("columnList", JavaList),
            new ClassMember("unique", JavaBoolean, false)
        );

        public static Annotation Table() => new Annotation("Table", "javax.persistence.Table",
            new ClassMember("name", JavaString),
            new ClassMember("schema", JavaString),
            new ClassMember("indexes", JavaList)
        );

        public static Annotation HvlEntitySequence() => new Annotation("HvlEntitySequence", "tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence",
            new ClassMember("name", JavaString)
        );

        public static Annotation OneToMany() => new Annotation("OneToMany", "javax.persistence.OneToMany",
           new ClassMember("cascade", JavaString),
           new ClassMember("mappedBy", JavaString)
        );
        public static Annotation ManyToOne() => new Annotation("ManyToOne", "javax.persistence.ManyToOne");
        public static Annotation OneToOne() => new Annotation("OneToOne", "javax.persistence.OneToOne");
        public static Annotation ManyToMany() => new Annotation("ManyToMany", "javax.persistence.ManyToMany");
        public static Annotation ForeignKey() => new Annotation("ForeignKey", "javax.persistence.ForeignKey",
            new ClassMember("name", JavaString)
        );

        public static Annotation JoinColumn() => new Annotation("JoinColumn", "javax.persistence.JoinColumn",
           new ClassMember("referencedColumnName", JavaString),
           new ClassMember("name", JavaString),
           new ClassMember("foreignKey", JavaObject)
       );
    }

}

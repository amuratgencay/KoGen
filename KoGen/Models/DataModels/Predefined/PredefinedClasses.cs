using KoGen.Models.ClassMembers;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;
using static KoGen.Extentions.ListExtentions;
using KoGen.Models.DataModels.Enum;
using System.Collections.Generic;
using System.Collections;

namespace KoGen.Models.DataModels.Predefined
{
    public class PredefinedClasses
    {
        public static readonly Class JavaObject = new Class
        {
            Name = "Object",
            Package = Package.DefaultPackage,
            Nullable = true,
            DefaultValue = null,
            AccessModifier = Public,
            NonAccessModifiers = new List<NonAccessModifier>(),
            ClassMembers = new ClassMemberCollection(JavaObject),
            Annotations = new List<Annotation>()
        };
        public static readonly Class JavaVoid = new Class("void");

        public static readonly Class JavaBoolean = new Class("Boolean", x => x != null ? (((bool)x) ? "true" : "false") : "", Final);

        public static readonly Class JavaBooleanPrimitive = new Class("name", false, false, x => ((bool)x) ? "true" : "false", Final);

        public static readonly Class JavaString = new Class("String", x => $"\"{x}\"", Final);

        public static readonly Class JavaCharacter = new Class("Character", Final);

        public static readonly Class JavaCharPrimitive = new Class("char", '\0', false);

        public static readonly Class JavaNumber = new Class("Number", Abstract);

        public static readonly Class JavaInteger = new Class("Integer", JavaNumber, Final);

        public static readonly Class JavaIntPrimitive = new Class("int", JavaNumber, 0, false);

        public static readonly Class JavaLong = new Class("Long", JavaNumber, Final);

        public static readonly Class JavaLongPrimitive = new Class("long", JavaNumber, 0, false);

        public static readonly Class JavaShort = new Class("Short", JavaNumber, Final);

        public static readonly Class JavaShortPrimitive = new Class("short", JavaNumber, 0, false);

        public static readonly Class JavaDouble = new Class("Double", JavaNumber, Final);

        public static readonly Class JavaDoublePrimitive = new Class("double", JavaNumber, 0.0, false);

        public static readonly Class JavaDate = new Class("Date", "java.util");

        public static readonly Class JavaList = new Class("List", "java.util", x => ConvertToList((IList)x).Aggregate(y => y.ToString(), ", ", "{ ", " }", true));

        public static readonly Class HvlEntity = new Class("HvlEntity", "tr.com.havelsan.nf.domain.model.entity.HvlEntity");

    }
}

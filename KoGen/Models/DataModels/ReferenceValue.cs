using KoGen.Extentions;
using static KoGen.Extentions.NullableExtentions;
using System;
using System.Collections;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
namespace KoGen.Models.DataModels
{
    public class ReferenceValue
    {
        public string Value { get; set; }
        public bool StaticMember { get; set; }
        public Class Clazz { get; set; }
        public override string ToString() => Value;
        public ReferenceValue(string value, bool staticMember = false, Class clazz = null)
        {
            Value = value;
            StaticMember = staticMember;
            Clazz = clazz ?? JavaObject;
        }
        private static ReferenceValue GetReferenceValue(Class c, ClassMember cm) =>
            new ReferenceValue($"{c.Name}.{cm.Name}", true, c);

        public static ReferenceValue FromStaticMember(Class c, string name) => 
            IfPresent(c.GetStaticMember(name), cm => GetReferenceValue(c,cm), null);

        public static ReferenceValue FromStaticMemberByValue(Class c, object value) =>
            IfPresent(c.GetStaticMemberByValue(value), cm => GetReferenceValue(c, cm), null);
        
    }
}

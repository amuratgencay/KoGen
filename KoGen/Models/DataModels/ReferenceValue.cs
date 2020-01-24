using KoGen.Extentions;
using System;
using System.Collections;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
namespace KoGen.Models.DataModels
{
    public class ReferenceValue
    {
        public ReferenceValue(string value, bool staticMember = false, Class clazz = null)
        {
            Value = value;
            StaticMember = staticMember;
            Clazz = clazz ?? JavaObject;
        }
        public static ReferenceValue FromStaticMember(Class c, string name)
        {
            var cm = c.GetStaticMember(name);
            if (cm == null) return null;
            return new ReferenceValue($"{c.Name}.{cm.Name}", true, c);
        }

        public static ReferenceValue FromStaticMemberByValue(Class c, object value)
        {
            ClassMember cm = c.GetStaticMemberByValue(value);
            if (cm == null) return null;
            return new ReferenceValue($"{c.Name}.{cm.Name}", true, c);
        }
        public string Value { get; set; }
        public bool StaticMember { get; set; }
        public Class Clazz { get; set; }
        public override string ToString() => Value;
    }
}

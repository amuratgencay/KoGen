using KoGen.Models.DataModels.Enum;
using System.Collections.Generic;

namespace KoGen.Models.DataModels.Predefined
{
    public class PredefinedClasses
    {
        public static readonly Class HvlEntity = new Class
        {
            Name = "HvlEntity",
            Package = "tr.com.havelsan.nf.domain.model.entity.HvlEntity",
        };

        public static readonly Class JavaObject = new Class
        {
            Name = "Object",
            Package = Package.DefaultPackage
        };

        public static readonly Class JavaBoolean = new Class
        {
            Name = "Boolean",
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaBooleanPrimitive = new Class
        {
            Name = "boolean",
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final },
            DefaultValue = false
        };

        public static readonly Class JavaString = new Class
        {
            Name = "String",
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final },           
        };

        public static readonly Class JavaCharacter = new Class
        {
            Name = "Character",
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaCharPrimitive = new Class
        {
            Name = "char",
            Package = Package.DefaultPackage,
            Nullable = false,
            DefaultValue = '\0'
        };

        public static readonly Class JavaNumber = new Class
        {
            Name = "Number",
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Abstract }
        };

        public static readonly Class JavaInteger = new Class
        {
            Name = "Integer",
            BaseClass = JavaNumber,
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaIntPrimitive = new Class
        {
            Name = "int",
            Package = Package.DefaultPackage,
            Nullable = false,
            DefaultValue = 0,
        };

        public static readonly Class JavaLong = new Class
        {
            Name = "Long",
            BaseClass = JavaNumber,
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaLongPrimitive = new Class
        {
            Name = "long",
            Package = Package.DefaultPackage,
            Nullable = false,
            DefaultValue = 0,
        };

        public static readonly Class JavaShort = new Class
        {
            Name = "Short",
            BaseClass = JavaNumber,
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaShortPrimitive = new Class
        {
            Name = "short",
            Package = Package.DefaultPackage,
            Nullable = false,
            DefaultValue = 0,
        };

        public static readonly Class JavaDouble = new Class
        {
            Name = "Double",
            BaseClass = JavaNumber,
            Package = Package.DefaultPackage,
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Final }
        };

        public static readonly Class JavaDoublePrimitive = new Class
        {
            Name = "double",
            Package = Package.DefaultPackage,
            Nullable = false,
            DefaultValue = 0.0,
        };


        public static readonly Class JavaDate = new Class
        {
            Name = "Date",
            Package = "java.util"            
        };

        public static readonly Class JavaList = new Class
        {
            Name = "List",
            Package = "java.util"
        };
    }
}

using KoGen.Extentions;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.Predefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen
{
    public class ConverterClass : Class
    {
        public ModelClass ModelClass { get; set; }
        public ConverterClass(ModelClass mClass, string module)
        {
            ModelClass = mClass;
            Name = mClass.EntityClass.EntityConstraints.TableName.ToPascalCase() + "Converter";
            Package = $@"havelsan.kovan.{module}.service.converter";

            BaseClass = PredefinedClasses.HvlConverter;
            BaseClass.GenericList.Add(mClass);
            BaseClass.GenericList.Add(mClass.EntityClass);

            var doConvertToModelFunc = new Function(this, "doConvertToModel", PredefinedClasses.JavaVoid, Models.DataModels.Enum.AccessModifier.Protected);
            var modelParam = new FunctionParameter("model", mClass);
            var entityParam = new FunctionParameter("entity", mClass.EntityClass);
            doConvertToModelFunc.FunctionParameters.Add(modelParam);
            doConvertToModelFunc.FunctionParameters.Add(entityParam);
            foreach (var member in modelParam.Type.ClassMembers.ClassMembers)
            {
                var modelMember = member;
                var entityMember = entityParam.Type.ClassMembers.ClassMembers.FirstOrDefault(x => x.Name == member.Name);
                doConvertToModelFunc.Expressions.Add(
                    new SetterAssignExpression
                    {
                        Destination = modelParam,
                        Source = entityParam,
                        DestinationClassMember = modelMember,
                        SourceClassMember = entityMember
                    });
            }
            Functions.Add(doConvertToModelFunc);

            var doConvertToEntityFunc = new Function(this, "doConvertToEntity", PredefinedClasses.JavaVoid, Models.DataModels.Enum.AccessModifier.Protected);
            modelParam = new FunctionParameter("model", mClass);
            entityParam = new FunctionParameter("entity", mClass.EntityClass);
            doConvertToEntityFunc.FunctionParameters.Add(entityParam);
            doConvertToEntityFunc.FunctionParameters.Add(modelParam);
            foreach (var member in modelParam.Type.ClassMembers.ClassMembers)
            {
                var modelMember = member;
                var entityMember = entityParam.Type.ClassMembers.ClassMembers.FirstOrDefault(x => x.Name == member.Name);
                doConvertToEntityFunc.Expressions.Add(
                    new SetterAssignExpression
                    {
                        Destination = entityParam,
                        Source = modelParam,
                        DestinationClassMember = entityMember,
                        SourceClassMember = modelMember
                    });
            }
            Functions.Add(doConvertToEntityFunc);

            //foreach (var classMember in EntityClass.ClassMembers.ClassMembers)
            //{
            //    var cm = new ClassMember(classMember.Name, classMember.Type, classMember.Value, classMember.AccessModifier, classMember.NonAccessModifiers.ToArray());
            //    foreach (var annotation in classMember.Annotations.Where(x => !x.Package.IsParent("javax.persistence")))
            //    {
            //        var an = new Annotation(annotation.Name, annotation.Package, annotation.Parameters.Values.ToArray());
            //        cm.Annotations.Add(an);
            //    }
            //    ClassMembers.Add(cm);
            //}
        }
    }
}
using KoGen.Extentions;
using KoGen.Models.ClassMembers;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.Predefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen
{
    public class ModelClass : Class
    {
        public EntityClass EntityClass { get; set; }
        public ModelClass(EntityClass eClass, string module)
        {
            EntityClass = eClass;
            Name = eClass.EntityConstraints.TableName.ToPascalCase() + "Model";
            Package = $@"havelsan.kovan.{module}.common.dto";

            BaseClass = PredefinedClasses.HvlModel;

            foreach (var classMember in EntityClass.ClassMembers.ClassMembers)
            {
                var cm = new ClassMember(classMember.Name, classMember.Type, classMember.Value, classMember.AccessModifier, classMember.NonAccessModifiers.ToArray());
                foreach (var annotation in classMember.Annotations.Where(x => !x.Package.IsParent("javax.persistence")))
                {
                    var an = new Annotation(annotation.Name, annotation.Package, annotation.Parameters.Values.ToArray());
                    cm.Annotations.Add(an);
                }
                ClassMembers.Add(cm);
            }
        }
    }
}

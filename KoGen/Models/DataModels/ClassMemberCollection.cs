using KoGen.Extentions;
using static KoGen.Extentions.StringExtentions;
using System.Collections.Generic;
using System.Linq;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;
using System;
using KoGen.Models.DataModels.Functions;

namespace KoGen.Models.DataModels
{
    [Serializable]
    public class ClassMemberCollection
    {
        public readonly Class Owner;
        public List<ClassMember> ClassMembers { get; set; }
        public List<GetterFunction> GetterFunctions { get; set; }
        public List<SetterFunction> SetterFunctions { get; set; }

        public ClassMemberCollection(Class owner)
        {
            Owner = owner;
            ClassMembers = new List<ClassMember>();
            GetterFunctions = new List<GetterFunction>();
            SetterFunctions = new List<SetterFunction>();
        }
        public ClassMemberCollection Add(ClassMember classMember, bool addGetterSetter = true)
        {
            ClassMembers.Add(classMember);
            classMember.Owner = Owner;
            if (!classMember.NonAccessModifiers.Contains(Abstract) && !classMember.NonAccessModifiers.Contains(Final) && !classMember.NonAccessModifiers.Contains(Static) && addGetterSetter)
            {
                GetterFunctions.Add(new GetterFunction(classMember));
                SetterFunctions.Add(new SetterFunction(classMember));
            }
            return this;
        }
        public ClassMemberCollection AddRange(IEnumerable<ClassMember> classMembers)
        {
            ClassMembers
                .AddList(classMembers)
                .ForEach(x =>
                {
                    x.Owner = Owner;
                    if (!x.NonAccessModifiers.Contains(Abstract) && !x.NonAccessModifiers.Contains(Final) && !x.NonAccessModifiers.Contains(Static))
                    {
                        GetterFunctions.Add(new GetterFunction(x));
                        SetterFunctions.Add(new SetterFunction(x));
                    }
                });
            return this;
        }
        public ClassMember GetStaticMember(string name)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(Static) && x.Name == name);
        }

        public ClassMember GetStaticMemberByValue(object value)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(Static) && x.Value == value);
        }

        public void Replace(string name, ClassMember classMember)
        {
            for (int i = 0; i < ClassMembers.Count; i++)
            {
                if (ClassMembers[i].Name == name)
                {
                    classMember.Owner = ClassMembers[i].Owner;

                    GetterFunctions.Remove(GetterFunctions.First(x => x.ClassMember.Name == ClassMembers[i].Name));
                    SetterFunctions.Remove(SetterFunctions.First(x => x.ClassMember.Name == ClassMembers[i].Name));
                    ClassMembers.RemoveAt(i);
                    GetterFunctions.Insert(i, new GetterFunction(classMember));
                    SetterFunctions.Insert(i, new SetterFunction(classMember));
                    ClassMembers.Insert(i, classMember);

                    break;
                }
            }
        }

        public void ChangeType(string name, Class type)
        {
            for (int i = 0; i < ClassMembers.Count; i++)
            {
                if (ClassMembers[i].Name == name)
                {

                    GetterFunctions.Remove(GetterFunctions.First(x => x.ClassMember.Name == ClassMembers[i].Name));
                    SetterFunctions.Remove(SetterFunctions.First(x => x.ClassMember.Name == ClassMembers[i].Name));
                    var classMember = ClassMembers[i];
                    if(classMember.Type == Predefined.PredefinedClasses.JavaList)
                    {
                        classMember.Type.GenericList.Clear();
                        classMember.Type.GenericList.Add(type);
                    }
                    else
                    {
                        classMember.Type = type;
                    }
                    GetterFunctions.Insert(i, new GetterFunction(classMember));
                    SetterFunctions.Insert(i, new SetterFunction(classMember));

                    break;
                }
            }
        }

        public List<Wrapper> Relations =>
           new List<Wrapper>()
               .AddList(ClassMembers.SelectMany(x => x.Annotations))
               .AddList(ClassMembers.Select(x => x.Type));


        public string ClassMembersString =>
            ClassMembers.Aggregate(x => "\t" + x.GetDeclaration(), DoubleNewLine, DoubleNewLine, NewLine);

    }
}

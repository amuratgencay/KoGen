using KoGen.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.ClassMembers
{
    public class Annotation : Wrapper
    {
        public Dictionary<string, ClassMember> Parameters { get; set; } = new Dictionary<string, ClassMember>();

        public Annotation(string name, Package package, params ClassMember[] parameters)
        {
            Name = name;
            Package = package;
            Parameters = parameters.Length > 0 ? parameters.ToDictionary(x => x.Name, y => y) : new Dictionary<string, ClassMember>();
        }
        public Annotation SetParameter(string param, object value)
        {
            Parameters[param].Value = value;
            return this;
        }
        public ClassMember this[string key]
        {
            get { return Parameters[key]; }
        }
        public override string ToString()
        {
            var res = $"@{Name}";
            if (Parameters.Count > 0)
            {
                res += "(";
                foreach (var key in Parameters.Keys)
                {
                    var assignStr = Parameters[key].AssignString();
                    if (!string.IsNullOrEmpty(assignStr) && (assignStr != "{}") && Parameters[key].Value != Parameters[key].DefaultValue)
                        res += (key + " = " + Parameters[key].AssignString() + ", ");
                }
                res = res.Remove(res.Length - 2);
                res += ")";
            }

            return res;
        }
    }

}

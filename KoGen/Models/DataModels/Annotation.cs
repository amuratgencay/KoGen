using KoGen.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.ClassMembers
{
    public class Annotation
    {
        public Package Package { get; set; }
        public string Name { get; set; }
        public Dictionary<string, ClassMember> Parameters { get; set; } = new Dictionary<string, ClassMember>();
        public Annotation SetParameter(string param, object value)
        {
            Parameters[param].Value = value;
            return this;
        }
        public override string ToString()
        {
            var res = $"@{Name}";
            if (Parameters.Count > 0)
            {
                res += "(";
                foreach (var key in Parameters.Keys)
                {
                    var assingStr = Parameters[key].AssingString();
                    if(!string.IsNullOrEmpty(assingStr) && (assingStr != "{}") &&  Parameters[key].Value != Parameters[key].DefaultValue)
                        res += ( key + " = " + Parameters[key].AssingString() + ", ");
                }
                res = res.Remove(res.Length - 2);
                res += ")";
            }

            return res;
        }
    }

}

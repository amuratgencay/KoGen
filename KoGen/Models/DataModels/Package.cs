using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.DataModels
{
    public class Package
    {
        public string Name { get; set; }
        public Package Parent { get; set; }
        public List<Package> Children { get; set; } = new List<Package>();
        public Package() { }
        public Package(string name) { Name = name; }

        private static readonly Package _root = new Package();

        public static readonly Package DefaultPackage = ContertToPackage("java.lang");

        private static Package ContertToPackage(string value)
        {
            var i = _root;
            foreach (var item in value.Split('.'))
            {
                if (i.Children.Any(x => x.Name == item))
                {
                    i = i.Children.FirstOrDefault(x => x.Name == item);
                }
                else
                {
                    var newPackage = new Package { Name = item, Parent = i };
                    i.Children.Add(newPackage);
                    i = newPackage;
                }

            }
            return i;
        }
        public static implicit operator Package(string value) => ContertToPackage(value);


        public override string ToString()
        {
            var res = "";
            for (var i = this; i.Parent != null; i = i.Parent)
            {
                res = (i.Parent != null && !string.IsNullOrEmpty(i.Parent.Name) ? "." : "") + i.Name + res;
            }
            return res;
        }
    }
}

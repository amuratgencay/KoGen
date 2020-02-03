using System;

namespace KoGen.Models.DataModels
{
    [Serializable]
    public abstract class Wrapper
    {
        public Package Package { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

    }
}

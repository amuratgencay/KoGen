using System;

namespace KoGen.Models.DatabaseModels
{
    [Serializable]
    public class Sequence
    {
        public string Name { get; set; }
        public long Start { get; set; }
        public long Increment { get; set; }
        public virtual Column Column { get; set; }

    }

}

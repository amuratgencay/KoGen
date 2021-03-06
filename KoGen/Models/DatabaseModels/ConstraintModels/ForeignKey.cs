﻿using System;

namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    [Serializable]
    public class ForeignKey : ConstraintBase
    {
        public Column Column { get; set; }
        public Column ReferenceColumn { get; set; }
    }

}

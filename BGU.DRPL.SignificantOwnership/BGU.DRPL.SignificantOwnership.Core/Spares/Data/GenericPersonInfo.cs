﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class GenericPersonInfo
    {
        public EntityType PersonType { get; set; }
        public PhysicalPersonInfo PhysicalPerson { get; set; }
        public LegalPersonInfo LegalPerson { get; set; }
    }
}
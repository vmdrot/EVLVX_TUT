using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.ValidationSchemes
{
    public class ValidationShemeBaseAttribute : Attribute
    {

        public string condition;
        public ValidationShemeBaseAttribute() { }
        public ValidationShemeBaseAttribute(string condition) { this.condition = condition; }
    }
}

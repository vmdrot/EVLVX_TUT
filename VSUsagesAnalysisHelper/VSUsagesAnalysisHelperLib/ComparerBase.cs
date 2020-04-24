using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public abstract class ComparerBase
    {
        protected int StrHash(string str)
        {
            return (str != null) ? str.GetHashCode() : 0;
        }

    }
}

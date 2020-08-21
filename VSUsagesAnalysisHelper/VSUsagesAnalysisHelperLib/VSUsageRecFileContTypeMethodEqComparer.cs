using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsageRecFileContTypeMethodEqComparer : ComparerBase, IEqualityComparer<VSUsageRec>
    {
        public bool Equals(VSUsageRec x, VSUsageRec y)
        {
            return (x.File == y.File
                && x.ContainingMember == y.ContainingMember
                && x.ContainingType == y.ContainingType);
        }

        public int GetHashCode(VSUsageRec obj)
        {
            return StrHash(obj.File)  + StrHash(obj.ContainingMember) + StrHash(obj.ContainingType);
        }

    }
}

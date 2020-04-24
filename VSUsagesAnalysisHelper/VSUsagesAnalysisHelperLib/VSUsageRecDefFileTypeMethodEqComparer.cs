using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsageRecDefFileTypeMethodEqComparer : ComparerBase, IEqualityComparer<VSUsageRec>
    {
        public bool Equals(VSUsageRec x, VSUsageRec y)
        {
            return (x.DefFile== y.DefFile
                && x.DefMember == y.DefMember
                && x.DefType== y.DefType);
        }

        public int GetHashCode(VSUsageRec obj)
        {
            return StrHash(obj.DefFile)  + StrHash(obj.DefMember) + StrHash(obj.DefType);
        }

    }
}

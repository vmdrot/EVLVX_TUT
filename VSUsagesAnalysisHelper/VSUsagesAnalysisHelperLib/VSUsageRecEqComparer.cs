using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsageRecEqComparer : ComparerBase, IEqualityComparer<VSUsageRec>
    {
        public bool Equals(VSUsageRec x, VSUsageRec y)
        {
            return (x.Code == y.Code
                && x.File == y.File
                && x.Line == y.Line
                && x.Column == y.Column
                && x.Project == y.Project
                && x.Kind == y.Kind
                && x.ContainingMember == y.ContainingMember
                && x.ContainingType == y.ContainingType);
        }

        public int GetHashCode(VSUsageRec obj)
        {
            return obj.Code.GetHashCode() + StrHash(obj.File) + obj.Line + obj.Column + StrHash(obj.Project)
            + StrHash(obj.Kind) + StrHash(obj.ContainingMember) + StrHash(obj.ContainingType);
        }

    }
}

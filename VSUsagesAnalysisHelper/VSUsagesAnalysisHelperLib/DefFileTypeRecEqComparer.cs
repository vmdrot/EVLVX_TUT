using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class DefFileTypeRecEqComparer : ComparerBase, IEqualityComparer<DefFileTypeRec>
    {

        public bool Equals(DefFileTypeRec x, DefFileTypeRec y)
        {
            return (x.DefFile == y.DefFile
                && x.DefType == y.DefType);
        }

        public int GetHashCode(DefFileTypeRec obj)
        {
            return StrHash( obj.DefFile) + StrHash( obj.DefType);
        }

    }
}

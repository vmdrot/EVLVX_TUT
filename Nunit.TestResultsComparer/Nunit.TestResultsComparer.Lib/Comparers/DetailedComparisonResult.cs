using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib.Comparer
{
    public class DetailedComparisonResult
    {
        private bool _isEmpty = false;
        private static readonly DetailedComparisonResult _empty = new DetailedComparisonResult() { _isEmpty = true };
        public bool IsEmpty()
        {
            return _isEmpty;
        }
        
        public static DetailedComparisonResult Empty
        { 
            get 
            {
                return _empty;
            } 
        }
    }
}

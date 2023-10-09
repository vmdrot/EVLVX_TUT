using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib.Data.Allure
{
    public interface IAllureStartStopItem
    {
        long start { get; set; }
        long stop { get; set; }
    }
}

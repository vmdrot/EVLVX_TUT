using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib.Data.Allure
{
    public class ScenariosContainersResultsHive
    {
        public Dictionary<string,ScenariosContainer> Containers { get; set; }
        public Dictionary<string,ScenarioRunResult> Results { get; set; }
        public Dictionary<string,string> Attachements { get; set; }
    }
}

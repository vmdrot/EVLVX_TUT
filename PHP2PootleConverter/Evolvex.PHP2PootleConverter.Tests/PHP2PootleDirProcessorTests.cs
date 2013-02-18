using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.PHP2PootleConverterLib;

namespace Evolvex.PHP2PootleConverter.Tests
{
    [TestFixture]
    public class PHP2PootleDirProcessorTests
    {
        [Test]
        public void ProcessDirPHP2POTest()
        {
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            p.Process(@"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo", ConvertDirection.PHP2Pootle);
        }
        [Test]
        public void ProcessDirPO2PHPTest()
        {
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            p.Process(@"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo", ConvertDirection.Pootle2PHP);
        }
    }
}

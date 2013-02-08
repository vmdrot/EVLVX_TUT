using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Evolvex.PHP2PootleConverter.Tests
{
    [TestFixture]
    public class PHP2PootleConverterTests
    {
        [Test]
        public void PHP2PootleTest()
        {
            Evolvex.PHP2PootleConverterLib.PHP2PootleConverter c = new Evolvex.PHP2PootleConverterLib.PHP2PootleConverter();
            c.ProcessFile(@"D:\home\vmdrot\Testing\PHP2PootleConverter\attribute.php", Evolvex.PHP2PootleConverterLib.ConvertDirection.PHP2Pootle);
        }
    }
}

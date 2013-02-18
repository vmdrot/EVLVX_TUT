using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.PHP2PootleConverterLib;
using Evolvex.PHP2PootleConverterLib.Data;

namespace Evolvex.PHP2PootleConverter.Tests
{
    [TestFixture]
    public class PHP2PootleDirProcessorTests
    {
        [Test]
        public void ProcessDirPHP2POTest_old()
        {
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            p.Process(@"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo", ConvertDirection.PHP2Pootle);
        }

        [Test]
        public void ProcessDirPO2PHPTest_old()
        {
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            p.Process(@"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo", ConvertDirection.Pootle2PHP);
        }

        [Test]
        public void ProcessDirPHP2POTest()
        {
            string dir = @"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo";
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            ConversionSettings settings = new ConversionSettings() { DeleteSourceFiles = true, Direction = ConvertDirection.PHP2Pootle, SaveAsEncoding = Encoding.UTF8, SourceDir = dir, TargetDir = dir, SourceLanguageName = "english", TargetLanguageName = "russian" };
            p.Process(settings);
        }

        [Test]
        public void ProcessDirPO2PHPTest()
        {
            string dir = @"D:\home\vmdrot\Testing\PHP2PootleConverter\target\opencartPo";
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            ConversionSettings settings = new ConversionSettings() { DeleteSourceFiles = true, Direction = ConvertDirection.Pootle2PHP, SaveAsEncoding = Encoding.UTF8, SourceDir = dir, TargetDir = dir, SourceLanguageName = "russian", TargetLanguageName = "russian"};
            p.Process(settings);
        }
    }
}

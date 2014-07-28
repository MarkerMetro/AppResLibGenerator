using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppResLibGenerator.Test
{
    [TestClass]
    public class ResxParser_TryGetResourceValue
    {
        ResxParser GetResxParser()
        {
            var document = XDocument.Parse(
@"<?xml version='1.0' encoding='utf-8'?>
<root>
  <data name='ApplicationTitle' xml:space='preserve'>
    <value>UnityProject</value>
  </data>
</root>");

            return new ResxParser(document);
        }

        [TestMethod]
        public void ExistingKey_ReturnValue()
        {
            var target = GetResxParser();

            var actual = target.TryGetResourceValue("ApplicationTitle");

            Assert.AreEqual("UnityProject", actual);
        }


        [TestMethod]
        public void NonExistingKey_ReturnNull()
        {
            var target = GetResxParser();

            var actual = target.TryGetResourceValue("NonExisting");

            Assert.IsNull(actual);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppResLibGenerator.Test
{
    [TestClass]
    public class LocalMapper_GetAppResLibFileName
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Whitespace_Throw()
        {
            var target = new LocaleMapper();

            target.GetAppResLibFileName(" ");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WrongType_Throw()
        {
            var target = new LocaleMapper();

            target.GetAppResLibFileName("test.en.txt");
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NoMappings_AppResourcesResx_Throw()
        {
            var target = new LocaleMapper();

            target.GetAppResLibFileName("AppResources.resx");
        }

        [TestMethod]
        public void AppResourcesResx_ReturnDefault()
        {
            var target = new LocaleMapper()
            {
                Mappings = new Tuple<string, string, string>[0]
            };

            var actual = target.GetAppResLibFileName("AppResources.resx");

            Assert.AreEqual("AppResLib.dll", actual);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AppResources_EN_NoMapping_Throw()
        {
            var target = new LocaleMapper()
            {
                Mappings = new Tuple<string, string, string>[0]
            };

            target.GetAppResLibFileName("AppResources.en.resx");
        }

        [TestMethod]
        public void AppResources_EN_US_ReturnMapped()
        {
            var target = new LocaleMapper()
            {
                Mappings = new []
                {
                    Tuple.Create("English (United States)", "en-US", "AppResLib.dll.0409.mui"),
                }
            };

            var actual = target.GetAppResLibFileName("AppResources.en-US.resx");

            Assert.AreEqual("AppResLib.dll.0409.mui", actual);
        }

        [TestMethod]
        public void AppResources_de_ReturnMappedFor_de_DE()
        {
            var target = new LocaleMapper()
            {
                Mappings = new[]
                {
                    Tuple.Create("German (Germany)", "de-DE", "AppResLib.dll.0407.mui"),
                }
            };

            var actual = target.GetAppResLibFileName("AppResources.de.resx");

            Assert.AreEqual("AppResLib.dll.0407.mui", actual);
        }

    }
}

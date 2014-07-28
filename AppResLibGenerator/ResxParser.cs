using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AppResLibGenerator
{
    public class ResxParser
    {
        readonly XDocument document;
        public ResxParser(string resXFileName)
        {
            document = XDocument.Load(resXFileName);
        }

        public ResxParser(XDocument document)
        {
            this.document = document;
        }

        public string TryGetResourceValue(string resourceKey)
        {
            var datas = document.Root.Descendants("data");

            var data = datas.FirstOrDefault(d =>
                {
                    var att = d.Attribute("name");
                    return att != null && att.Value == resourceKey;
                });

            if (data != null)
            {
                var value = data.Element("value");
                if (value != null)
                    return value.Value;
            }

            return null;
        }
    }
}

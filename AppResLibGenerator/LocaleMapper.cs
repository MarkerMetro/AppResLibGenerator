using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppResLibGenerator
{
    public class LocaleMapper
    {
        public Tuple<string, string, string>[] Mappings { get; set; }

        public void Parse(string localeMappingsFileName)
        {
            var lines = File.ReadAllLines(localeMappingsFileName);

            try
            {
                var q = from l in lines
                        where !string.IsNullOrWhiteSpace(l)
                        let parts = l.Split(',')
                        select Tuple.Create(parts[0], parts[1], parts[2]);

                Mappings = q.ToArray();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                throw new InvalidDataException("Locale mappings file is invalid - must contain 3 column lines (example: English (United States),en-US,AppResLib.dll.0409.mui)", ex);
            }
        }

        public string GetAppResLibFileName(string resxFileName)
        {
            if (string.IsNullOrWhiteSpace(resxFileName) ||
                !Path.GetExtension(resxFileName).Equals(".resx", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Must specify RESX file");

            if (Mappings == null)
                throw new InvalidOperationException("Must load locale mappings before making this call");

            var languageCode = Path.GetExtension(Path.GetFileNameWithoutExtension(resxFileName)).TrimStart('.');
            if (string.IsNullOrEmpty(languageCode))
                return "AppResLib.dll";

            var mapping = Mappings.FirstOrDefault(m => m.Item2.Equals(languageCode, StringComparison.OrdinalIgnoreCase));

            if (mapping == null && !languageCode.Contains('-'))
            {
                var prefix = string.Concat(languageCode, "-");

                mapping = Mappings.FirstOrDefault(m => m.Item2.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
            }

            if(mapping==null)
                throw new ApplicationException(string.Format("Mapping not found for the language {0}. You can edit LocaleMappings.csv to add mapping for it.", languageCode));

            return mapping.Item3;
        }

    }
}

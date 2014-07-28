using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppResLibGenerator
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                    PrintUsage();

                var inputFilePath = args[0];

                var generator = new Generator()
                {
                    ResXFileName = inputFilePath
                };

                generator.Run();

                Console.WriteLine("Generated: {0}", generator.AppResLibFileName);
                Console.WriteLine("With Resources:");
                Console.WriteLine("- 100: Read from key '{0}', value: '{1}'", generator.Resource100Key, generator.Resource100Value);
                Console.WriteLine("- 101: Read from key '{0}', value: '{1}'", generator.Resource101Key, generator.Resource101Value);
                Console.WriteLine("- 102: Read from key '{0}', value: '{1}'", generator.Resource102Key, generator.Resource102Value);

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("AppResLibGenerator error: {0}", ex.Message);
                Trace.TraceError(ex.ToString());

                return ex.HResult;
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("AppResLibGenerator.exe [file path to .resx]");

            throw new ApplicationException("Invalid usage");
        }
    }
}

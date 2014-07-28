using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppResLibGenerator
{
    static class Interop
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr BeginUpdateResource(
            string pFileName,
           [MarshalAs(UnmanagedType.Bool)]bool bDeleteExistingResources);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UpdateResource(
            IntPtr hUpdate, 
            IntPtr lpType, 
            IntPtr lpName, 
            ushort wLanguage, 
            IntPtr lpData, 
            uint cbData);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool EndUpdateResource(IntPtr hUpdate, bool fDiscard);
    }
}

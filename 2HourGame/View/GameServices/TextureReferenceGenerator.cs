using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CustomTool;
using Microsoft.Win32;

namespace _2HourGame.View.GameServices
{
    [Guid("E4DD3207-BC41-4a58-A13C-6148736F4B70")]
    [ComVisible(true)]
    class TextureReferenceGenerator : BaseCodeGeneratorWithSite
    {

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            string code = "Generated Code Should Be Placed Here";
            return System.Text.Encoding.ASCII.GetBytes(code);
        }
    }
}

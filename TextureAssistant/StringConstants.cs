using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureAssistant
{
    static class StringConstants
    {
        public const string enumTemplate = 
@"public enum Content 
{
{0}
}";

        public const string loadTemplate = @"textureManager.LoadTexture(Content.{0}, ""{1}"");";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureAssistant
{
    class ContentInfo
    {
        public readonly string pathFromContent;
        public readonly string enumName;

        public ContentInfo(string pathFromContent) 
        {
            this.pathFromContent = pathFromContent.Substring(0, pathFromContent.LastIndexOf('.'));
            enumName = pathFromContent.Split('\\').Last().Split('.').First();
            enumName = char.ToUpper(enumName[0]) + enumName.Substring(1, enumName.Length - 1);
        }

        public static int CompateContentInfos(ContentInfo x, ContentInfo y)
        {
            return x.enumName.CompareTo(y.enumName);
        }
    }
}

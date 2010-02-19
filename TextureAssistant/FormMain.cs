using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TextureAssistant
{
    public partial class MainForm : Form
    {
        private List<ContentInfo> contents;

        public MainForm()
        {
            InitializeComponent();
        }

        private List<string> recursiveFindFiles(string directory)
        {
            List<string> files = new List<string>();
            foreach (string dir in Directory.GetDirectories(directory))
            {
                files.AddRange(recursiveFindFiles(dir));
            }

            List<string> foundFiles = new List<string>();
            foundFiles.AddRange(Directory.GetFiles(directory, "*.png"));
            foundFiles.AddRange(Directory.GetFiles(directory, "*.tga"));
            foreach (string file in foundFiles)
            {
                files.Add(file.Remove(0, TextBoxContentFolderLocation.Text.Length));
            }

            return files;
        }

        private void GenerateTextureCode_Click(object sender, EventArgs e)
        {
            if (TextBoxContentFolderLocation.Text[TextBoxContentFolderLocation.Text.Length - 1] != '\\')
                TextBoxContentFolderLocation.Text += "\\";
                
            List<string> fileNames = recursiveFindFiles(TextBoxContentFolderLocation.Text);

            contents = new List<ContentInfo>();
            foreach (string fileName in fileNames)
                contents.Add(new ContentInfo(fileName));
            contents.Sort(ContentInfo.CompateContentInfos);

            string enumString = String.Empty;
            foreach (ContentInfo c in contents) 
            {
                if(enumString != String.Empty)
                    enumString += ",\n";
                enumString += c.enumName;
            }
            RichTextBoxTextureCode.Text = StringConstants.enumTemplate.Replace("{0}", enumString);

            string loadContent = string.Empty;
            foreach (ContentInfo c in contents)
            {
                if (loadContent != String.Empty)
                    loadContent += "\n";
                loadContent += string.Format(StringConstants.loadTemplate, c.enumName, c.pathFromContent.Replace("\\", "\\\\"));
            }
            RichTextBoxLoads.Text = loadContent;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Microsoft.VisualBasic;

namespace TranslationUnifier
{
    public class ResourceReader : TranslationsUtilites
    {
        public ResourceReader(string path) : base(path)
        {

        }

        public void ReadRessourceFiles()
        {
            List<string> files = Directory.GetFiles(DirectoryPath, "*.resx", SearchOption.AllDirectories).ToList();
            List<string> filesToExlude = files.Where(x => x.Contains("Migration")).ToList();
            files = files.Except(filesToExlude).ToList();
            foreach (string file in files)
            {
                ReadRessource(file);
            }
        }

        public void ReadRessource(string path)
        {
            Language language;
            string name;
            if (CheckInputFile(path, out language, out name))
            {
                ResXResourceReader rsxr = new ResXResourceReader(path);

                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (DictionaryEntry entry in rsxr)
                {
                    dict.Add(entry.Key.ToString(), entry.Value.ToString());
                }
                rsxr.Close();
                FillDictionary(dict, language, name);
            }
        }
    }
}

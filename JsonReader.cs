using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationUnifier
{
    public class JsonReader : TranslationsUtilites
    {
        public JsonReader(string path) : base(path)
        {

        }

        public void ReadJsonFiles()
        {
            List<string> files = Directory.GetFiles(DirectoryPath, "*.json", SearchOption.AllDirectories).ToList();
            foreach (string file in files)
            {
                ReadJson(file);
            }
        }

        public void ReadJson(string path)
        {
            Language language;
            string name;
            if (CheckInputFile(path, out language, out name))
            {
                string json = File.ReadAllText(path);
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                FillDictionary(values, language, name);
            }
        }

        public void SaveFiles()
        {
            foreach(var el in Translations)
            {
                List<string> files = Directory.GetFiles(DirectoryPath, string.Format("{0}*.json", el.Key), SearchOption.AllDirectories).ToList();

                foreach (var lang in el.Value)
                {
                    string fileName = string.Format("{0}.{1}.json", el.Key, LanguageUtilities.GetLanguageCode(lang.Key));
                    string file = files.Where(x => x.Contains(fileName)).FirstOrDefault();
                    if (file != null)
                    {
                        string content = JsonConvert.SerializeObject(lang.Value, Formatting.Indented);
                        File.WriteAllText(file, content);
                    }
                }
            }
        }
    }
}

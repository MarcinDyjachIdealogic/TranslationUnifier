using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationUnifier
{
    public abstract class TranslationsUtilites
    {
        public Dictionary<string, Dictionary<Language, Dictionary<string, string>>> Translations { get; private set; }
        public string DirectoryPath { get; set; }

        public TranslationsUtilites(string path)
        {
            DirectoryPath = path;
            Translations = new Dictionary<string, Dictionary<Language, Dictionary<string, string>>>();
        }

        public string GetName(string path)
        {
            return Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
        }

        public bool CheckInputFile(string path, out Language language, out string fileName)
        {
            language = LanguageUtilities.GetLanguage(path);
            fileName = string.Empty;

            if (language != Language.None)
            {
                fileName = GetName(path);
                return true;
            }

            return false;
        }

        public void FillDictionary(Dictionary<string, string> dict, Language language, string name)
        {
            if (!Translations.ContainsKey(name))
            {
                Translations.Add(name, new Dictionary<Language, Dictionary<string, string>>());
            }
            if (Translations[name].ContainsKey(language))
            {
                foreach (var el in dict)
                {
                    if (!Translations[name][language].ContainsKey(el.Key))
                        Translations[name][language].Add(el.Key, el.Value);
                }
            }
            else
            {
                Translations[name].Add(language, dict);
            }

        }

        public void CompareTranslations(Dictionary<string, Dictionary<Language, Dictionary<string, string>>> others)
        {
            for (int i = 0; i < Translations.Count; i++)
            {
                string fileName = Translations.Keys.ElementAt(i);
                var values = Translations[fileName];
                for (int j = 0; j < values.Count; j++)
                {
                    Language language = values.Keys.ElementAt(j);
                    var translationInLanguage = values[language];
                    for (int k = 0; k < translationInLanguage.Count; k++)
                    {
                        string name = translationInLanguage.Keys.ElementAt(k);
                        string value = translationInLanguage[name];

                        var translationsForLanguage = others.Values.Select(x => x.Where(y => y.Key == language));
                        var dict = translationsForLanguage.SelectMany(x => x.SelectMany(y => y.Value));
                        var translations = dict.Where(x => x.Key == name);
                        
                        if (translations != null && translations.Any())
                        {
                            if (!translations.Any(x => x.Value == value))
                            {
                                Translations[fileName][language][name] = translations.First().Value;
                            }
                        }
                    }
                }
            }
        }
    }
}

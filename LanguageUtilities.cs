using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationUnifier
{
    public enum Language
    {
        None = 0,
        English = 1,
        French = 2,
        German = 3,
        Italian = 4,
        Norawy = 5
    }

    public class LanguageUtilities
    {
        public static Language GetLanguage(string fileName)
        {
            string language = Path.GetExtension(Path.GetFileNameWithoutExtension(fileName)).TrimStart('.');
            switch (language)
            {
                case "en":
                    return Language.English;
                case "fr":
                    return Language.French;
                case "de":
                    return Language.German;
                case "it":
                    return Language.Italian;
                case "no":
                    return Language.Norawy;
                default:
                    return Language.None;
            }
        }

        public static string GetLanguageCode(Language language)
        {
            switch (language)
            {
                case Language.English:
                    return "en";
                case Language.French:
                    return "fr";
                case Language.German:
                    return "de";
                case Language.Italian:
                    return "it";
                case Language.Norawy:
                    return "no";
                default:
                    return string.Empty;
            }
        }
    }
}

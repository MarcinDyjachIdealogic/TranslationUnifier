using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationUnifier
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceReader resourceReader = new ResourceReader(args[1]);
            resourceReader.ReadRessourceFiles();
            JsonReader jsonReader = new JsonReader(args[2]);
            jsonReader.ReadJsonFiles();

            jsonReader.CompareTranslations(resourceReader.Translations);

            jsonReader.SaveFiles();
        }
    }
}

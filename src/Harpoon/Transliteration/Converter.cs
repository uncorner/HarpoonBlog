using System.Collections.Generic;

namespace Transliteration
{
    public static class Converter
    {
        // ГОСТ 16876-71
        private static readonly IDictionary<string, string> GOST = new Dictionary<string, string>();
        // ISO 9-95
        private static readonly IDictionary<string, string> ISO = new Dictionary<string, string>();

        public static string Front(string text)
        {
            return Front(text, TransliterationType.ISO);
        }

        public static string Front(string text, TransliterationType type)
        {
            var output = text;
            var tdict = GetDictionaryByType(type);

            foreach (var key in tdict)
            {
                output = output.Replace(key.Key, key.Value);
            }
            return output;
        }

        public static string Back(string text)
        {
            return Back(text, TransliterationType.ISO);
        }

        public static string Back(string text, TransliterationType type)
        {
            var output = text;
            var tdict = GetDictionaryByType(type);

            foreach (var key in tdict)
            {
                output = output.Replace(key.Value, key.Key);
            }
            return output;
        }

        private static IDictionary<string, string> GetDictionaryByType(TransliterationType type)
        {
            var tdict = ISO;
            if (type == TransliterationType.Gost) tdict = GOST;
            return tdict;
        }

        static Converter()
        {
            GOST.Add("Є", "EH");
            GOST.Add("І", "I");
            GOST.Add("і", "i");
            GOST.Add("№", "#");
            GOST.Add("є", "eh");
            GOST.Add("А", "A");
            GOST.Add("Б", "B");
            GOST.Add("В", "V");
            GOST.Add("Г", "G");
            GOST.Add("Д", "D");
            GOST.Add("Е", "E");
            GOST.Add("Ё", "JO");
            GOST.Add("Ж", "ZH");
            GOST.Add("З", "Z");
            GOST.Add("И", "I");
            GOST.Add("Й", "JJ");
            GOST.Add("К", "K");
            GOST.Add("Л", "L");
            GOST.Add("М", "M");
            GOST.Add("Н", "N");
            GOST.Add("О", "O");
            GOST.Add("П", "P");
            GOST.Add("Р", "R");
            GOST.Add("С", "S");
            GOST.Add("Т", "T");
            GOST.Add("У", "U");
            GOST.Add("Ф", "F");
            GOST.Add("Х", "KH");
            GOST.Add("Ц", "C");
            GOST.Add("Ч", "CH");
            GOST.Add("Ш", "SH");
            GOST.Add("Щ", "SHH");
            GOST.Add("Ъ", "'");
            GOST.Add("Ы", "Y");
            GOST.Add("Ь", "");
            GOST.Add("Э", "EH");
            GOST.Add("Ю", "YU");
            GOST.Add("Я", "YA");
            GOST.Add("а", "a");
            GOST.Add("б", "b");
            GOST.Add("в", "v");
            GOST.Add("г", "g");
            GOST.Add("д", "d");
            GOST.Add("е", "e");
            GOST.Add("ё", "jo");
            GOST.Add("ж", "zh");
            GOST.Add("з", "z");
            GOST.Add("и", "i");
            GOST.Add("й", "jj");
            GOST.Add("к", "k");
            GOST.Add("л", "l");
            GOST.Add("м", "m");
            GOST.Add("н", "n");
            GOST.Add("о", "o");
            GOST.Add("п", "p");
            GOST.Add("р", "r");
            GOST.Add("с", "s");
            GOST.Add("т", "t");
            GOST.Add("у", "u");
            GOST.Add("ф", "f");
            GOST.Add("х", "kh");
            GOST.Add("ц", "c");
            GOST.Add("ч", "ch");
            GOST.Add("ш", "sh");
            GOST.Add("щ", "shh");
            GOST.Add("ъ", "");
            GOST.Add("ы", "y");
            GOST.Add("ь", "");
            GOST.Add("э", "eh");
            GOST.Add("ю", "yu");
            GOST.Add("я", "ya");
            GOST.Add("«", "");
            GOST.Add("»", "");
            GOST.Add("—", "-");

            ISO.Add("Є", "YE");
            ISO.Add("І", "I");
            ISO.Add("Ѓ", "G");
            ISO.Add("і", "i");
            ISO.Add("№", "#");
            ISO.Add("є", "ye");
            ISO.Add("ѓ", "g");
            ISO.Add("А", "A");
            ISO.Add("Б", "B");
            ISO.Add("В", "V");
            ISO.Add("Г", "G");
            ISO.Add("Д", "D");
            ISO.Add("Е", "E");
            ISO.Add("Ё", "YO");
            ISO.Add("Ж", "ZH");
            ISO.Add("З", "Z");
            ISO.Add("И", "I");
            ISO.Add("Й", "J");
            ISO.Add("К", "K");
            ISO.Add("Л", "L");
            ISO.Add("М", "M");
            ISO.Add("Н", "N");
            ISO.Add("О", "O");
            ISO.Add("П", "P");
            ISO.Add("Р", "R");
            ISO.Add("С", "S");
            ISO.Add("Т", "T");
            ISO.Add("У", "U");
            ISO.Add("Ф", "F");
            ISO.Add("Х", "X");
            ISO.Add("Ц", "C");
            ISO.Add("Ч", "CH");
            ISO.Add("Ш", "SH");
            ISO.Add("Щ", "SHH");
            ISO.Add("Ъ", "'");
            ISO.Add("Ы", "Y");
            ISO.Add("Ь", "");
            ISO.Add("Э", "E");
            ISO.Add("Ю", "YU");
            ISO.Add("Я", "YA");
            ISO.Add("а", "a");
            ISO.Add("б", "b");
            ISO.Add("в", "v");
            ISO.Add("г", "g");
            ISO.Add("д", "d");
            ISO.Add("е", "e");
            ISO.Add("ё", "yo");
            ISO.Add("ж", "zh");
            ISO.Add("з", "z");
            ISO.Add("и", "i");
            ISO.Add("й", "j");
            ISO.Add("к", "k");
            ISO.Add("л", "l");
            ISO.Add("м", "m");
            ISO.Add("н", "n");
            ISO.Add("о", "o");
            ISO.Add("п", "p");
            ISO.Add("р", "r");
            ISO.Add("с", "s");
            ISO.Add("т", "t");
            ISO.Add("у", "u");
            ISO.Add("ф", "f");
            ISO.Add("х", "x");
            ISO.Add("ц", "c");
            ISO.Add("ч", "ch");
            ISO.Add("ш", "sh");
            ISO.Add("щ", "shh");
            ISO.Add("ъ", "");
            ISO.Add("ы", "y");
            ISO.Add("ь", "");
            ISO.Add("э", "e");
            ISO.Add("ю", "yu");
            ISO.Add("я", "ya");
            ISO.Add("«", "");
            ISO.Add("»", "");
            ISO.Add("—", "-");
        }
    }
    
}
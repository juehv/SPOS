using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SimplePOS.Preferences
{
    //TODO deprecated ... backuppen und weg
    class PreferenceParser
    {
        private static string FILENAME = "options.txt";
        private static string SYNTAX_REGEX = "\\w+=\\w+";
        private static string COMMENT_CHAR = "#";

        public static void loadFile(){
            List<string> options = new List<string>();
            Regex keyValueSyntax = new Regex(SYNTAX_REGEX);

            // check if file exists
            if (!File.Exists(FILENAME))
            {
                File.Create(FILENAME);
            }

            // read the file
            using (TextReader reader = new StreamReader(FILENAME))
            {
                String input;
                while ((input = reader.ReadLine()) != null)
                {
                    // check Syntax
                    if (input.StartsWith(COMMENT_CHAR))
                    {
                        continue;
                    }
                    if (keyValueSyntax.IsMatch(input))
                    {
                        options.Add(input);
                    }
                }
            }
            parseList(options);
        }

        public static void parseList(List<string> options)
        {

        }
    }
}

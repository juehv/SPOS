// Type: SimplePOS.Preferences.PreferenceParser
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SimplePOS.Preferences
{
  internal class PreferenceParser
  {
    private static string FILENAME = "options.txt";
    private static string SYNTAX_REGEX = "\\w+=\\w+";
    private static string COMMENT_CHAR = "#";

    static PreferenceParser()
    {
    }

    public static void loadFile()
    {
      List<string> options = new List<string>();
      Regex regex = new Regex(PreferenceParser.SYNTAX_REGEX);
      if (!File.Exists(PreferenceParser.FILENAME))
        File.Create(PreferenceParser.FILENAME);
      using (TextReader textReader = (TextReader) new StreamReader(PreferenceParser.FILENAME))
      {
        string input;
        while ((input = textReader.ReadLine()) != null)
        {
          if (!input.StartsWith(PreferenceParser.COMMENT_CHAR) && regex.IsMatch(input))
            options.Add(input);
        }
      }
      PreferenceParser.parseList(options);
    }

    public static void parseList(List<string> options)
    {
    }
  }
}

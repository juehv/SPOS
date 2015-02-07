// Type: SimplePOS.Preferences.PreferenceManager
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

namespace SimplePOS.Preferences
{
  internal class PreferenceManager
  {
    private static bool doubleprint = false;
    private static string version = "0.1.0.6";

    public static bool DOUBLEPRINT
    {
      get
      {
        return PreferenceManager.doubleprint;
      }
      set
      {
        PreferenceManager.doubleprint = value;
      }
    }

    public static string VERSION
    {
      get
      {
        return PreferenceManager.version;
      }
      set
      {
        PreferenceManager.version = value;
      }
    }

    static PreferenceManager()
    {
    }
  }
}

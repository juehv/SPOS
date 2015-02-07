// Type: SimplePOS.NumberManager
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Database;
using System;

namespace SimplePOS
{
  public class NumberManager
  {
    private static NumberManager INSTANCE = (NumberManager) null;

    static NumberManager()
    {
    }

    private NumberManager()
    {
    }

    public static NumberManager getInstance()
    {
      if (NumberManager.INSTANCE == null)
        NumberManager.INSTANCE = new NumberManager();
      return NumberManager.INSTANCE;
    }

    public long getNextNumberOfCurrentYear()
    {
      NumberCounter counter = DAO_.getInstance().getCounter(DateTime.Now.Year);
      long num = counter.tick();
      DAO_.getInstance().setCounter(counter);
      return num;
    }
  }
}

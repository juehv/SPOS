// Type: SimplePOS.NumberCounter
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

namespace SimplePOS
{
  public class NumberCounter
  {
    private long count = 0L;
    private int year;

    public int Year
    {
      get
      {
        return this.year;
      }
    }

    public long Count
    {
      get
      {
        return this.count;
      }
      set
      {
        this.count = value;
      }
    }

    public NumberCounter(int year)
    {
      this.year = year;
    }

    public long tick()
    {
      return this.count++;
    }
  }
}

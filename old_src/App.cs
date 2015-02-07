// Type: SimplePOS.App
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public class App : Application
  {
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
    }

    [DebuggerNonUserCode]
    [STAThread]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}

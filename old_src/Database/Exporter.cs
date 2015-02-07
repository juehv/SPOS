// Type: SimplePOS.Database.Exporter
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS;
using System;
using System.IO;
using System.Text;

namespace SimplePOS.Database
{
  internal class Exporter
  {
    private static Exporter Instance = (Exporter) null;

    public static Exporter INSTANCE
    {
      get
      {
        if (Exporter.Instance == null)
          Exporter.Instance = new Exporter();
        return Exporter.Instance;
      }
    }

    static Exporter()
    {
    }

    private Exporter()
    {
    }

    public string CreateExportString()
    {
      StringBuilder stringBuilder = new StringBuilder("Beleg-Nr.;Datum;Rechnungsbetrag;Artikel-Nr.;Artikel-Beschreibung;Artikel-Menge;Artikel-Einzelpreis;Artikel-Gesamtpreis;\n");
      foreach (SaveableInvoice saveableInvoice in DAO_.getInstance().getAllInvoices())
      {
        string str1 = saveableInvoice.Number.ToString();
        string str2 = saveableInvoice.Date.ToString("dd.MM.yyyy");
        string amountString = saveableInvoice.AmountString;
        foreach (string str3 in saveableInvoice.Items)
        {
          stringBuilder.Append(str1).Append(";").Append(str2).Append(";").Append(amountString).Append(";");
          stringBuilder.Append(str3);
          stringBuilder.Append("\n");
        }
        stringBuilder.Append(";;;;;;;;\n");
      }
      Console.WriteLine(((object) stringBuilder).ToString());
      return ((object) stringBuilder).ToString();
    }

    public void ExportInvoiceToFile(string filepath)
    {
      string exportString = this.CreateExportString();
      TextWriter textWriter = (TextWriter) new StreamWriter(filepath);
      textWriter.WriteLine(exportString);
      textWriter.Close();
    }
  }
}

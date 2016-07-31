using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using SimplePOS.Invoicing;

namespace SimplePOS.Printing
{
    public class XmlInvoiceGenerator
    {
        private string _pathXml = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/Invoice.xml";

        private string _pathXsl = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/LayoutXSL.xsl";

        private string _pathResult = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/InvoiceResult.html";

        public XmlInvoiceGenerator(SimplePOS.Invoicing.SerializableInvoice serialinvoice)
        {

            XmlWriter w = XmlWriter.Create(_pathXml);

            w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"LayoutXSL.xsl\"");

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableInvoice));
            writer.Serialize(w, serialinvoice);
            w.Close();

            XslCompiledTransform myXslTrans = new XslCompiledTransform();

            myXslTrans.Load(_pathXsl);

            myXslTrans.Transform(_pathXml, _pathResult);

        }

        public void PrintXmlInvoice()
        {

            System.Windows.Forms.WebBrowser webBrowserForPrinting = new System.Windows.Forms.WebBrowser();

            webBrowserForPrinting.Url = new Uri(_pathResult);

            webBrowserForPrinting.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDocument);

        }

        private void PrintDocument(object sender,
                    WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((System.Windows.Forms.WebBrowser)sender).ShowPrintDialog();

            // Dispose the WebBrowser now that the task is complete. 
            ((System.Windows.Forms.WebBrowser)sender).Dispose();
        }

    }
}

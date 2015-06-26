using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace SimplePOS.Invoicing
{
    public class XMLInvoiceGenerator
    {
        private string pathXML = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/Invoice.xml";

        private string pathXSL = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/LayoutXSL.xsl";

        private string pathResult = System.Windows.Forms.Application.StartupPath + "/InvoiceDocs/InvoiceResult.html";

        public XMLInvoiceGenerator(SimplePOS.Invoicing.SerializableInvoice serialinvoice)
        {

            XmlWriter w = XmlWriter.Create(pathXML);

            w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"LayoutXSL.xsl\"");

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableInvoice));
            writer.Serialize(w, serialinvoice);
            w.Close();

            XslCompiledTransform myXslTrans = new XslCompiledTransform();

            myXslTrans.Load(pathXSL);

            myXslTrans.Transform(pathXML, pathResult);

        }

        public void PrintXMLInvoice()
        {

            System.Windows.Forms.WebBrowser webBrowserForPrinting = new System.Windows.Forms.WebBrowser();

            webBrowserForPrinting.Url = new Uri(pathResult);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using SimplePOS.Preferences;

namespace SimplePOS.Printing
{
    /// <summary>
    /// Class for printing invoices
    /// TODO umstellen auf xslt prozessor
    /// </summary>
    public class InvoicePrinter
    {
        private static InvoicePrinter INSTANCE = null;

        private InvoicePrinter()
        {
            //empty
        }

        public static InvoicePrinter getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new InvoicePrinter();
            }
            return INSTANCE;
        }

        /// <summary>
        /// Prints the invoice
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool print(SimplePOS.Invoicing.Invoice items)
        {
            foreach (SimplePOS.Invoicing.InvoiceItem item in items.Items)
            {
                Console.WriteLine(item.Article.Name + " x" + item.Quantity);
            }


            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                List<SimplePOS.Invoicing.Invoice> pages = PrintingPreprocessor.ProcessInvoices(items);
                foreach (SimplePOS.Invoicing.Invoice item in pages)
                {
                    A4 page = new A4(item);
                    page.Measure(new System.Windows.Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight));
                    page.Arrange(new System.Windows.Rect(new System.Windows.Point(0, 0), page.DesiredSize));
                    pd.PrintVisual(page.getGrid(), item.PageNumber);

                    if (PreferenceManager.DOUBLEPRINT)
                    {
                        pd.PrintVisual(page.getGrid(), item.PageNumber);
                    }
                }

                return true;
            }
            return false;
        }

    }
}

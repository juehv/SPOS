using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Printing
{
    /// <summary>
    /// Class for implementing some helper funktions
    /// needed for multipage printing
    /// </summary>
    class PrintingPreprocessor
    {
        public static int CheckLinesLeft(List<string> list)
        {
            int retval = A4.LINES;
            retval -= list.Count;

            return retval;
        }

        private static bool fitsInPage(int lines)
        {
            // entscheidung welches seitenlayout benutzt wird
            return (A4.LINES >= lines);

        }

        public static List<SimplePOS.Invoicing.Invoice> ProcessInvoices(SimplePOS.Invoicing.Invoice invoice)
        {
            List<SimplePOS.Invoicing.Invoice> pages = new List<SimplePOS.Invoicing.Invoice>();

            // Lines berechnen
            int lines = 0;
            foreach (SimplePOS.Invoicing.InvoiceItem item in invoice.Items)
            {
                lines++;
                if (item.Quantity > 1) lines++;
            }

            // Checken obs auf eine Seite passt
            if (!fitsInPage(lines))
            {
                // prepare item stacks
                //List<List<InvoiceItem>> itemStacks = new List<List<InvoiceItem>>();
                //List<InvoiceItem> rawData = invoice.Items;
                lines = 0;
                int pageCount = 1;
                List<SimplePOS.Invoicing.InvoiceItem> aktStack = new List<SimplePOS.Invoicing.InvoiceItem>();
                foreach (SimplePOS.Invoicing.InvoiceItem item in invoice.Items)
                {
                    lines++;
                    if (item.Quantity > 1) lines++;

                    if (fitsInPage(lines))
                    {
                        aktStack.Add(item);
                    }
                    else
                    {
                        lines = 0;
                        //itemStacks.Add(aktStack);
                        pages.Add(new SimplePOS.Invoicing.Invoice(invoice, pageCount++, aktStack, invoice.TaxSet1, invoice.TaxSet2,invoice.ShowTax));
                        aktStack = new List<SimplePOS.Invoicing.InvoiceItem>();
                        aktStack.Add(item);
                    }
                }
                if (aktStack.Count > 0)
                {
                    pages.Add(new SimplePOS.Invoicing.Invoice(invoice, pageCount++, aktStack, invoice.TaxSet1, invoice.TaxSet2, invoice.ShowTax));
                }


            }
            else pages.Add(invoice);



            return pages;
        }
    }
}

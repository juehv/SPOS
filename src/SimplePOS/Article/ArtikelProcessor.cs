using SimplePOS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SimplePOS.Article
{
    /// <summary>
    /// Son funktions for processing invoide article.
    /// </summary>
    class ArtikelProcessor
    {

        // artikelnummer analyse
        public static AbstractArticle.ArticleType convertToType(string number)
        {
            if (Discount.isDiscountTag(number))
            {
                return AbstractArticle.ArticleType.DISCOUNT;
            }
            else
            {
                return AbstractArticle.ArticleType.ARTICLE;
            }
        }

        // TAG analayse oder datenbankabfrage
        public static AbstractArticle preprocessArtikel(Window parent, ISposDb db, string number)
        {
            AbstractArticle article = null;

            switch (convertToType(number))
            {
                case AbstractArticle.ArticleType.ARTICLE:
                    article = db.GetArticleByNumber(number);
                    if (article == null)
                    {
                        // Artikel ist nicht vorhanden --> neu anlegen
                        ArticleView window = new ArticleView(db,new RegularArticle(number));
                        window.Owner = parent;
                        window.ShowDialog();
                        article = db.GetArticleByNumber(number);
                        // wenn nichts gültiges eingegeben wird --> beenden
                        if (article == null)
                        {
                            return null;
                        }
                    }

                    break;
                case AbstractArticle.ArticleType.DISCOUNT:
                    int discountInCent = Discount.getAmountOfTag(number);
                    if (discountInCent < 0)
                    {
                        // Wenn der Tag keinen Wertenthält muss eine Nutzereingabe erfolgen
                        DiscountInputBox widow = new DiscountInputBox();
                        widow.Owner = parent;
                        int userValue = widow.ShowWithResult();
                        if (userValue > 0)
                        {
                            discountInCent = userValue;
                        }
                        else
                        {
                            // Wenn keine korrekte Eingabe erfolgt -> Ende.
                            return null;
                        }
                    }
                    article = new Discount(discountInCent);

                    break;
            }


            return article;
        }

        // rechnung bearbeiten wenn sonderartikel vorhanden
        // zb der zukünftige % rabatt
        public static void postprocessArtikel(ref List<SimplePOS.Invoicing.InvoiceItem> items){

        }
    }
}

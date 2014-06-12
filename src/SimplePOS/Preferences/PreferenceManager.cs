using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Preferences
{
    /// <summary>
    /// Static class for holding some global options
    /// </summary>
    class PreferenceManager
    {
        private PreferenceManager() {/*empty*/}
        /// <summary>
        /// Indikates if a invoice should be printed twice
        /// </summary>
        public static bool DOUBLEPRINT = false;
        /// <summary>
        /// The version number
        /// </summary>
        public static string VERSION = "0.2.0.0";
        /// <summary>
        /// Currency symbol for forms
        /// </summary>
        public static string CURRENCY_FORM = "€";
        /// <summary>
        /// Currency symbol for technical use (e.g. database)
        /// </summary>
        public static string CURRENCY_TECH = "EUR";
        /// <summary>
        /// Formatstring for datetime
        /// </summary>
        public static string DATE_FORMAT_STRING = "dd.MM.yyyy";
        /// <summary>
        /// predefined TAX
        /// </summary>
        public static double TAX_1 = 19;
        public static double TAX_2 = 7;
        /// <summary>
        /// Start value of invoice numbering
        /// </summary>
        public static long INVOICE_NUMBER_START = 10000;


    }
}

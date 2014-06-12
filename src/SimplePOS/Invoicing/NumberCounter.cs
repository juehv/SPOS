using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Invoicing
{
    //TODO remove
    public class NumberCounter
    {
        private int year;
        private long count = 0;

        public NumberCounter(int year)
        {
            this.year = year;
        }

        public int Year
        {
            get { return this.year; }
        }

        public long Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public long tick()
        {
            return count++;
        }
    }
}

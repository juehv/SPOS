using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplePOS.Database;

namespace SimplePOS.Invoicing
{
    // TODO remove
    public class NumberManager
    {
        private static NumberManager INSTANCE = null;

        private NumberManager()
        {
            //empty
        }

        public static NumberManager getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new NumberManager();
            }
            return INSTANCE;
        }

        public long getNextNumberOfCurrentYear()
        {
            NumberCounter counter = DAO.getInstance().getCounter(DateTime.Now.Year);
            long retval = counter.tick();
            DAO.getInstance().setCounter(counter);
            return retval;
        }

    }
}

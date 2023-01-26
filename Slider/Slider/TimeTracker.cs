using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider
{
    public class TimeTracker
    {
        private DateTime startJoc;

        private DateTime stopJoc;

        public DateTime getStartJoc()
        {
            return startJoc;
        }

        public void setStartJoc(DateTime startJoc)
        {
            this.startJoc = startJoc;
        }

        public DateTime getStopJoc()
        {
            return stopJoc;
        }

        public void setStopJoc(DateTime stopJoc)
        {
            this.stopJoc = stopJoc;
        }
    }

}
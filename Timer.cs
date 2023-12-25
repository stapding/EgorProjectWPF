using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Rus
{
    class Timer
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private TextBlock time = new TextBlock();

        public Timer(TextBlock time) {
            this.time = time;
            tick(null, null);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += tick;
            timer.Start();
        }


        public void tick(object sender, EventArgs e)
        {
            DateTime may = new DateTime(2023, 5, 1);
            TimeSpan span = may.Subtract(DateTime.Now);
            time.Text = $"{span.Days} дней {span.Hours} часов {span.Minutes} минут до старта марафона";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pacman_c_sharp
{
    class OnOffTimer
    {
        private DateTime startTime = DateTime.UtcNow;
        public bool Activate(int millisecOn, int millisecOff)
        {
            var durationOn = new TimeSpan(0, 0, 0, 0, millisecOn);
            var durationOff = new TimeSpan(0, 0, 0, 0, millisecOff);

            TimeSpan duration = DateTime.UtcNow - startTime;

            if (duration.Duration() > (durationOn + durationOff))
            {
                startTime = DateTime.UtcNow;
            }

            return duration.Duration() < durationOn;
        }
    }
}
